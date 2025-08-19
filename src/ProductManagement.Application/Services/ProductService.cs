using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Enums;
using ProductManagement.Core.Exceptions;
using ProductManagement.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace ProductManagement.Application.Services;

public class ProductService : IProductService {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync(
            includes: p => p.Supplier);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetAllAsync(
            p => p.Id == id,
            includes: p => p.Supplier);

        return _mapper.Map<ProductResponseDto>(product.FirstOrDefault());
    }

    public async Task<ProductResponseDto> CreateProductAsync(ProductRequestDto productDto)
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

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> UpdateProductAsync(ProductRequestDto productDto)
    {
        // Check if supplier exists first
        var supplierExists = await _unitOfWork.SupplierRepository.ExistsAsync(x => x.Id == productDto.SupplierId);
        if (!supplierExists)
        {
            throw new KeyNotFoundException($"Supplier with ID {productDto.SupplierId} not found");
        }


        var product = await _unitOfWork.ProductRepository.GetByIdAsync(productDto.Id);
        if (product == null)
            throw new NotFoundException(nameof(Product), productDto.Id);

        _mapper.Map(productDto, product);
        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException(nameof(Product), id);

        await _unitOfWork.ProductRepository.DeleteAsync(product);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string? name,
    QuantityPerUnit? quantityPerUnit,
    int? reorderLevel,
    string? supplierName,
    double? unitPrice,
    int? unitsInStock,
    int? unitsOnOrder)
    {
        // Build the predicate dynamically
        Expression<Func<Product, bool>> predicate = p =>
            (string.IsNullOrEmpty(name) || p.Name.Contains(name)) &&
            (!quantityPerUnit.HasValue || p.QuantityPerUnit == quantityPerUnit.Value) &&
            (!reorderLevel.HasValue || p.ReorderLevel == reorderLevel.Value) &&
            (string.IsNullOrEmpty(supplierName) || p.Supplier.Name.Contains(supplierName)) &&
            (!unitPrice.HasValue || p.UnitPrice == unitPrice.Value) &&
            (!unitsInStock.HasValue || p.UnitsInStock == unitsInStock.Value) &&
            (!unitsOnOrder.HasValue || p.UnitsOnOrder == unitsOnOrder.Value);

        // Fetch data with includes and filtering
        var products = await _unitOfWork.ProductRepository.GetAllAsync(
            predicate,
            p => p.Supplier  // Include Supplier
        );

        // Map to DTO
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);


    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsNeedReorderAsync()
    {
        Expression<Func<Product, bool>> predicate = p => p.UnitsInStock <= p.ReorderLevel;
       var products = await _unitOfWork.ProductRepository.GetAllAsync(predicate);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);

    }

   

    public async Task<ProductResponseDto> GetProductWithMinOrdersAsync()
    {
        var product =( await _unitOfWork.ProductRepository.GetAllAsync())
                       .OrderBy(p => p.UnitsOnOrder)
                       .FirstOrDefault();
        return _mapper.Map<ProductResponseDto>(product);
    }


}