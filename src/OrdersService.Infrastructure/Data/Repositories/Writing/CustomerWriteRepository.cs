using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Entities;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Writing;

public class CustomerWriteRepository(OrdersDbContext context) : WriteRepository<Customer>(context), ICustomerWriteRepository
{
    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _context.Customers.FirstOrDefaultAsync(_ => _.Email == email);
    }
}
