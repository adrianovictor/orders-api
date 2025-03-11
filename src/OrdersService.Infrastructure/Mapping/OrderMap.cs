using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Mapping;

public class OrderMap : EntityMap<Order>
{
    protected override void Map(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.CustomerId).IsRequired();
        builder.Property(_ => _.OrderDate).IsRequired().HasColumnType("datetime2");
        builder.Property(_ => _.TotalAmount).IsRequired().HasColumnType("numeric(18,2)");
        builder.Property(_ => _.Status).IsRequired();

        builder.HasMany(e => e.Items)
                      .WithOne(e => e.Order)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);        
    }
}
