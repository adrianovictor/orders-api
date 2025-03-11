using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OrdersService.Application.Services;

public class SyncDataHostedService(IMediator mediator, 
    IConfiguration configuration, 
    IServiceScopeFactory serviceScopeFactory) : BackgroundService, IDisposable
{
    private readonly IMediator _mediator = mediator;
    private readonly IConfiguration _configuration = configuration;
    private readonly IServiceScopeFactory _serviceProvider = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Publish(new SyncDataNotification("Synchronizing data between SQL and MongoDB."), stoppingToken);
            }

            // Aguarda antes da próxima execução
            await Task.Delay(TimeSpan.FromMinutes(_configuration.GetValue<int>("DataSync:IntervalMinutes")), stoppingToken);
        }
    }

}
