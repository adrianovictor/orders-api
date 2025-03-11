using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Mapping;

public class ProductMap : EntityMap<Product>
{
    protected override void Map(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Name).IsRequired().HasMaxLength(120).IsUnicode(false);
        builder.Property(_ => _.Price).IsRequired().HasColumnType("numeric(18,2)");
    }
}
