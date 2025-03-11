using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Mapping;

public class OrderItemMap : EntityMap<OrderItem>
{
    protected override void Map(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.OrderId).IsRequired();
        builder.Property(_ => _.ProductId).IsRequired();
        builder.Property(_ => _.ProductName).IsRequired().HasMaxLength(120).IsUnicode(false);
        builder.Property(_ => _.Quantity).IsRequired();
        builder.Property(_ => _.UnitPrice).IsRequired().HasColumnType("numeric(18,2)");
        builder.Property(_ => _.TotalPrice).IsRequired().HasColumnType("numeric(18,2)");

        builder.HasIndex(_ => new { _.OrderId }, "FK_ORDER_ITEM_ORDER_ID");
        builder.HasIndex(_ => new { _.ProductId }, "FK_ORDER_ITEM_PRODUCT_ID");
    }
}
