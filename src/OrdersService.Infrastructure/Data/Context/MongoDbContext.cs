using MongoDB.Driver;
using OrdersService.Domain.Models;
using OrdersService.Infrastructure.Data.Settings;

namespace OrdersService.Infrastructure.Data.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoSettings settings)
    {
        var mongoClient = new MongoClient(settings.ConnectionString);
        _database = mongoClient.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<CustomerDto> Customers => _database.GetCollection<CustomerDto>("Customers");
    public IMongoCollection<ProductDto> Products => _database.GetCollection<ProductDto>("Products");
    public IMongoCollection<OrderDto> Orders => _database.GetCollection<OrderDto>("Orders");
}
