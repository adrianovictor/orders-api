using OrdersService.Domain.Entities;

namespace OrdersService.Domain.Interfaces.Repository.Writing;

public interface IOrderWriteRepository : IWriteRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
    Task<Order> GetOrderWithItemsAsync(int id);
}
