using System;
using OrdersService.Domain.Models;

namespace OrdersService.Domain.Interfaces.Repository.Reading;

public interface IOrderReadRepository
{
    Task<OrderDto> GetByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetAllAsync();
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(int customerId);
    Task AddAsync(OrderDto orderDto);
    Task UpdateAsync(OrderDto orderDto);
    Task RemoveAsync(int id);
}
