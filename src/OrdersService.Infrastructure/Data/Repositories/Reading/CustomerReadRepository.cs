using MongoDB.Driver;
using OrdersService.Domain.Models;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Reading;

public class CustomerReadRepository(MongoDbContext context) : ICustomerReadRepository
{
    private readonly MongoDbContext _context = context;

    public async Task AddAsync(CustomerDto entity)
    {
        await _context.Customers.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<CustomerDto>.Filter.Eq(_ => _.Id, id);
        await _context.Customers.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        return await _context.Customers.Find(_ => true).ToListAsync();
    }

    public Task<CustomerDto> GetByEmailAsync(string email)
    {
        var filter = Builders<CustomerDto>.Filter.Eq(_ => _.Email, email);
        return _context.Customers.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<CustomerDto> GetByIdAsync(int id)
    {
        var filter = Builders<CustomerDto>.Filter.Eq(o => o.Id, id);
        return await _context.Customers.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CustomerDto>> GetCustomersByIdsAsync(IEnumerable<int> ids)
    {
        var filter = Builders<CustomerDto>.Filter.In(o => o.Id, ids);
        return await _context.Customers.Find(filter).ToListAsync();
    }

    public async Task UpdateAsync(CustomerDto entity)
    {
        var filter = Builders<CustomerDto>.Filter.Eq(_ => _.Id, entity.Id);
        await _context.Customers.ReplaceOneAsync(filter, entity);
    }
}
