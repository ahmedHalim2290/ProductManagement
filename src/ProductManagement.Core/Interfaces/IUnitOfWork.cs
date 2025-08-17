using ProductManagement.Core.Entities;

namespace ProductManagement.Core.Interfaces;
public interface IUnitOfWork : IDisposable {
    IRepository<Product> ProductRepository { get; }
    IRepository<Supplier> SupplierRepository { get; }
    Task<int> CompleteAsync();
}