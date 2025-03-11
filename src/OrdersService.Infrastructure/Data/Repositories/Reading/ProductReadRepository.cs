using MongoDB.Driver;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;
using OrdersService.Infrastructure.Data.Context;

namespace OrdersService.Infrastructure.Data.Repositories.Reading;

public class ProductReadRepository(MongoDbContext context) : IProductReadRepository
{
    private readonly MongoDbContext _context = context;

    public async Task AddAsync(ProductDto entity)
    {
        await _context.Products.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<ProductDto>.Filter.Eq(_ => _.Id, id);
        await _context.Products.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await _context.Products.Find(_ => true).ToListAsync();
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var filter = Builders<ProductDto>.Filter.Eq(o => o.Id, id);
        return await _context.Products.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(ProductDto entity)
    {
        var filter = Builders<ProductDto>.Filter.Eq(_ => _.Id, entity.Id);
        await _context.Products.ReplaceOneAsync(filter, entity);
    }
}
