using MongoDB.Driver;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Reading;

public class OrderReadRepository(MongoDbContext context) : IOrderReadRepository
{
    private readonly MongoDbContext _context = context;

    public async Task AddAsync(OrderDto orderDto)
    {
        await _context.Orders.InsertOneAsync(orderDto);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        return await _context.Orders.Find(_ => true).ToListAsync();
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        var filter = Builders<OrderDto>.Filter.Eq(o => o.Id, id);
        return await _context.Orders.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
    {
        var filter = Builders<OrderDto>.Filter.Eq(o => o.CustomerId, customerId);
        return await _context.Orders.Find(filter).ToListAsync();
    }    

    public async Task RemoveAsync(int id)
    {
        var filter = Builders<OrderDto>.Filter.Eq(o => o.Id, id);
        await _context.Orders.DeleteOneAsync(filter);
    }

    public async Task UpdateAsync(OrderDto orderDto)
    {
        var filter = Builders<OrderDto>.Filter.Eq(o => o.Id, orderDto.Id);
        await _context.Orders.ReplaceOneAsync(filter, orderDto);
    }
}
