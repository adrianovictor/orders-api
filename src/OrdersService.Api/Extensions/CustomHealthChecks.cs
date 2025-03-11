using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OrdersService.Api.Extensions;

public class CustomHealthChecks : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var health = new HealthCheckResult(status: HealthStatus.Healthy, description: "API Healthy");
            return Task.FromResult(health);
        }
        catch (Exception ex)
        {
            var health = new HealthCheckResult(status: HealthStatus.Unhealthy, description: ex.Message, exception: ex);
            return Task.FromResult(health);
        }
    }
}
