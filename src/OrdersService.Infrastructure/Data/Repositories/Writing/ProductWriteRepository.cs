using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Writing;

public class ProductWriteRepository(OrdersDbContext context) : WriteRepository<Product>(context), IProductWriteRepository
{
    public async Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> ids)
    {
        return await _context.Products.Where(_ => ids.Contains(_.Id)).ToListAsync();
    }
}
