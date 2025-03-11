namespace OrdersService.Infrastructure.Data.Settings;

public class MongoSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set;}
    public string OrdersCollection { get; set; }
}
