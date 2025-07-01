namespace OrdersService.Domain.Core;

public abstract class Entity<TEntity> : IEntity<TEntity> where TEntity : class
{
    public int Id { get; set; }

    public virtual bool IsPersisted()
    {
        return !IsTransient();
    }

    protected virtual bool IsTransient()
    {
        return Id == 0;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TEntity> other)
            return false;

        return Id == other.Id;
    }    
}
