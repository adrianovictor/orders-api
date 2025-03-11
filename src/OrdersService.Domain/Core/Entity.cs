using System;

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
}
