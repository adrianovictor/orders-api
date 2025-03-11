using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Writing;

public class WriteRepository<T>(OrdersDbContext context) : IWriteRepository<T> where T : class
{
    protected readonly OrdersDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}
