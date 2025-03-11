using OrdersService.Application.Configuration;

namespace OrdersService.Api.Extensions;

public static class HealthCheckExtensionCollection
{
    private static string namespaceApi = string.Empty;
    public static IServiceCollection AddHealthCheckExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddCheck<CustomHealthChecks>("Conexão com o Banco de dados", null, ["X.X.X"]);

        var css = configuration.GetValue<string>("HealthCheck:Css");
        var logo = configuration.GetValue<string>("HealthCheck:Logo");
        var location = configuration.GetValue<string>("HealthCheck:Location");
        var maximumHistoryCache = int.Parse(configuration.GetValue<string>("HealthCheck:MaximumHistoryCache"));
        var pollingSeconds = int.Parse(configuration.GetValue<string>("HealthCheck:PollingSeconds"));
        var maximumTimeCache = int.Parse(configuration.GetValue<string>("HealthCheck:MaximumTimeCache"));

        var healthCheckConfig = new HealthCheckConfig(css, logo, location, maximumHistoryCache, pollingSeconds, maximumTimeCache);

        if (!healthCheckConfig.IsValid()) throw new ApplicationException("Caminho do Health Check não está parametrizado no appsettings");

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(healthCheckConfig.PollingSeconds);
            options.MaximumHistoryEntriesPerEndpoint(healthCheckConfig.MaximumHistoryCache);
            options.AddHealthCheckEndpoint("API Template", healthCheckConfig.Location);
            options.SetHeaderText("API Template");
        })
        .AddInMemoryStorage(options =>
        {
            options.ConfigureLoggingCacheTime(TimeSpan.FromSeconds(healthCheckConfig.MaximumTimeCache));
        });

        return services;
    }
}
