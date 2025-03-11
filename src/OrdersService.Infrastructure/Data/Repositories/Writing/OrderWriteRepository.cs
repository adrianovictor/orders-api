using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Writing;

public class OrderWriteRepository(OrdersDbContext context) : WriteRepository<Order>(context), IOrderWriteRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Order> GetOrderWithItemsAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
