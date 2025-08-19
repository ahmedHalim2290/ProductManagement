using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces;

public interface IProductService {
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task UpdateProductAsync(int id, ProductDto productDto);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
    // Statistics methods
    Task<IEnumerable<ProductDto>> GetProductsNeedReorderAsync();
    Task<SupplierDto> GetLargestSupplierAsync();
    Task<ProductDto> GetProductWithMinOrdersAsync();
}