using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;
using System.Linq.Expressions;

namespace ProductManagement.Infrastructure.Data.Repositories;
public class Repository<T> : IRepository<T> where T : BaseEntity {
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;

    public Repository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _entities.AsQueryable();
        return await includes.Aggregate(query, (current, include) => current.Include(include))
                            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = _entities.Where(predicate);
        return await includes.Aggregate(query, (current, include) => current.Include(include))
                            .ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity)
    {
        _entities.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.AnyAsync(predicate);
    }
}