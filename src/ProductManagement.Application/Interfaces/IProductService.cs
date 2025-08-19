using ProductManagement.Application.DTOs;
using ProductManagement.Core.Enums;

namespace ProductManagement.Application.Interfaces;

public interface IProductService {
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto> GetProductByIdAsync(int id);
    Task<ProductResponseDto> CreateProductAsync(ProductRequestDto productDto);
    Task<ProductResponseDto> UpdateProductAsync(ProductRequestDto productDto);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string? name,
    QuantityPerUnit? quantityPerUnit,
    int? reorderLevel,
    string? supplierName,
    double? unitPrice,
    int? unitsInStock,
    int? unitsOnOrder);
    // Statistics methods
    Task<IEnumerable<ProductResponseDto>> GetProductsNeedReorderAsync();
    Task<SupplierResponseDto> GetLargestSupplierAsync();
    Task<ProductResponseDto> GetProductWithMinOrdersAsync();
}