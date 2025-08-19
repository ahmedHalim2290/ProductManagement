using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces;
public interface ISupplierService {
    Task<IEnumerable<SupplierRequestDto>> GetAllSuppliersAsync();
    Task<SupplierRequestDto> GetSupplierByIdAsync(int id);
    Task<SupplierRequestDto> CreateSupplierAsync(SupplierRequestDto supplierDto);
    Task UpdateSupplierAsync(SupplierRequestDto supplierDto);
    Task DeleteSupplierAsync(int id);
    Task<int> GetProductCountBySupplierAsync(int supplierId);
}