using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Exceptions;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Services;

public class ProductService : IProductService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync(
            includes: p => p.Supplier);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetAllAsync(
            p => p.Id == id,
            includes: p => p.Supplier);

        return _mapper.Map<ProductDto>(product.FirstOrDefault());
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        // Check if supplier exists first
        var supplierExists = await _unitOfWork.SupplierRepository.ExistsAsync(x => x.Id == productDto.SupplierId);
        if (!supplierExists)
        {
            throw new KeyNotFoundException($"Supplier with ID {productDto.SupplierId} not found");
        }

        var product = _mapper.Map<Product>(productDto);

        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task UpdateProductAsync(int id, ProductDto productDto)
    {
        // Check if supplier exists first
        var supplierExists = await _unitOfWork.SupplierRepository.ExistsAsync(x => x.Id == productDto.SupplierId);
        if (!supplierExists)
        {
            throw new KeyNotFoundException($"Supplier with ID {productDto.SupplierId} not found");
        }


        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException(nameof(Product), id);

        _mapper.Map(productDto, product);
        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException(nameof(Product), id);

        await _unitOfWork.ProductRepository.DeleteAsync(product);
        await _unitOfWork.CompleteAsync();
    }

    public Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDto>> GetProductsNeedReorderAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SupplierDto> GetLargestSupplierAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> GetProductWithMinOrdersAsync()
    {
        throw new NotImplementedException();
    }


}