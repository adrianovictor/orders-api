using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.UoW;

public class UnitOfWork(OrdersDbContext context) : IUnitOfWork
{
    private readonly OrdersDbContext _context = context;

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
