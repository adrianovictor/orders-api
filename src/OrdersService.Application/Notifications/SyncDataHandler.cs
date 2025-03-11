using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Notifications;

public class SyncDataHandler(IServiceScopeFactory serviceScopeFactory) : INotificationHandler<SyncDataNotification>
{
    private readonly IServiceScopeFactory _scopeFactory = serviceScopeFactory;

    public async Task Handle(SyncDataNotification notification, CancellationToken cancellationToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            await SyncCustomersData(scope, cancellationToken);
        }
    }

    private async Task SyncCustomersData(IServiceScope serviceScope, CancellationToken cancellationToken)
    {
        var repositoryWrite = serviceScope.ServiceProvider.GetRequiredService<ICustomerWriteRepository>();
        var repositoryRead = serviceScope.ServiceProvider.GetRequiredService<ICustomerReadRepository>();

        var customersWrite = await repositoryWrite.GetAllAsync();
        if (!customersWrite.Any())
        {
            return;
        }

        var customerRead = await repositoryRead.GetAllAsync();

        var missingItems = !customerRead.Any() ? 
            customersWrite.Select(_ => new CustomerDto { Id = _.Id, Name = _.Name, Email = _.Email, Phone = _.Phone }).ToList() :
            customerRead.Where(cr => !customersWrite.Any(cw => cw.Id == cr.Id))
            .ToList();

        foreach (var item in missingItems)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            try
            {
                await repositoryRead.AddAsync(new CustomerDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Phone = item.Phone
                });
                Console.WriteLine($"Cliente sincronizado: {item.Id} - {item.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao sincronizar cliente {item.Id}: {ex.Message}");
            }
        }
    }
}