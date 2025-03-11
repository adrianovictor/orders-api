using OrdersService.Domain.Entities;

namespace OrdersService.Domain.Interfaces.Repository.Writing;

public interface ICustomerWriteRepository : IWriteRepository<Customer>
{
    Task<Customer> GetByEmailAsync(string email);
}
