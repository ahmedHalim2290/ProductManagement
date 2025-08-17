using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces;
public interface ISupplierService {
    Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
    Task<SupplierDto> GetSupplierByIdAsync(int id);
    Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto);
    Task UpdateSupplierAsync(int id, SupplierDto supplierDto);
    Task DeleteSupplierAsync(int id);
    Task<int> GetProductCountBySupplierAsync(int supplierId);
}