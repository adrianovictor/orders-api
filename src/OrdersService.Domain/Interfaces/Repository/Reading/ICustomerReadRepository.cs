using OrdersService.Domain.Models;

namespace OrdersService.Domain.Interfaces.Repository.Reading;

public interface ICustomerReadRepository : IReadRepository<CustomerDto>
{
    Task<CustomerDto> GetByEmailAsync(string email);
    Task<IEnumerable<CustomerDto>> GetCustomersByIdsAsync(IEnumerable<int> ids);
}
