using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Exceptions;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Services;
public class SupplierService : ISupplierService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
    {
        var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync(
            includes: s => s.Products);

        var supplierDtos = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);

        // Map product count
        foreach (var supplierDto in supplierDtos)
        {
            supplierDto.ProductCount = suppliers
                .First(s => s.Id == supplierDto.Id).Products.Count;
        }

        return supplierDtos;
    }

    public async Task<SupplierDto> GetSupplierByIdAsync(int id)
    {
        var supplier = (await _unitOfWork.SupplierRepository.GetAllAsync(
            s => s.Id == id,
            includes: s => s.Products)).FirstOrDefault();

        if (supplier == null)
            throw new NotFoundException(nameof(Supplier), id);

        var supplierDto = _mapper.Map<SupplierDto>(supplier);
        supplierDto.ProductCount = supplier.Products.Count;

        return supplierDto;
    }

    public async Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto)
    {
        var supplier = _mapper.Map<Supplier>(supplierDto);

        await _unitOfWork.SupplierRepository.AddAsync(supplier);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<SupplierDto>(supplier);
    }

    public async Task UpdateSupplierAsync(int id, SupplierDto supplierDto)
    {
        var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
        if (supplier == null)
            throw new NotFoundException(nameof(Supplier), id);

        _mapper.Map(supplierDto, supplier);
        await _unitOfWork.SupplierRepository.UpdateAsync(supplier);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
        if (supplier == null)
            throw new NotFoundException(nameof(Supplier), id);

        // Check if supplier has products
        var products = await _unitOfWork.ProductRepository.GetAllAsync(p => p.SupplierId == id);
        if (products.Any())
            throw new InvalidOperationException("Cannot delete supplier with existing products");

        await _unitOfWork.SupplierRepository.DeleteAsync(supplier);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<int> GetProductCountBySupplierAsync(int supplierId)
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync(p => p.SupplierId == supplierId);
        return products.Count();
    }
}