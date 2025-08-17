using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using ProductManagement.Infrastructure.Data.Repositories;

namespace ProductManagement.Infrastructure.Data;
public class UnitOfWork(AppDbContext context, IRepository<Product> productRepository, IRepository<Supplier> supplierRepository) : IUnitOfWork {
    private readonly AppDbContext _context = context;
    private IRepository<Product> _productRepository = productRepository;
    private IRepository<Supplier> _supplierRepository = supplierRepository;

    public IRepository<Product> ProductRepository =>
        _productRepository ??= new Repository<Product>(_context);

    public IRepository<Supplier> SupplierRepository =>
        _supplierRepository ??= new Repository<Supplier>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}