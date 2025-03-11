namespace OrdersService.Domain.Interfaces.Repository.Reading;

public interface IReadRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);    
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}
