using OrdersService.Domain.Entities;

namespace OrdersService.Domain.Interfaces.Repository.Writing;

public interface IProductWriteRepository : IWriteRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> ids);
}
