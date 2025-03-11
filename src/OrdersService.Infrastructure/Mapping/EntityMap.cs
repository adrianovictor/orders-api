using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Core;

namespace OrdersService.Infrastructure.Mapping;

public abstract class EntityMap<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TEntity>
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        Map(builder);
    }

    protected abstract void Map(EntityTypeBuilder<TEntity> builder);
}
