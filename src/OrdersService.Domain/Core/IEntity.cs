using System;

namespace OrdersService.Domain.Core;

public interface IEntity<in TEntity> where TEntity : class
{
    int Id { get; }

    bool IsPersisted();
}
