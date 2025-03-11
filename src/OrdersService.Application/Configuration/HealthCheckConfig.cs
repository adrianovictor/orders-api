namespace OrdersService.Application.Configuration;

public class HealthCheckConfig(string css, string logo, string location, int maximumHistoryCache, int pollingSeconds, int maximumTimeCache)
{
    public string Css { get; set; } = css;
    public string Logo { get; set; } = logo;
    public string Location { get; set; } = location;
    public int MaximumTimeCache { get; set; } = maximumTimeCache;
    public int MaximumHistoryCache { get; set; } = maximumHistoryCache;
    public int PollingSeconds { get; set; } = pollingSeconds;
    public bool IsValid() =>
                !string.IsNullOrEmpty(Css) &&
                !string.IsNullOrEmpty(Logo) &&
                !string.IsNullOrEmpty(Location) &&
                MaximumHistoryCache > 0 &&
                PollingSeconds > 0;
}
