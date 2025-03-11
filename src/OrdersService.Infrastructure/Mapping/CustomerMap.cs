using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersService.Domain.Entities;

namespace OrdersService.Infrastructure.Mapping;

public class CustomerMap : EntityMap<Customer>
{
    protected override void Map(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Name).IsRequired().HasMaxLength(120).IsUnicode(false);
        builder.Property(_ => _.Email).IsRequired().HasMaxLength(255).IsUnicode(false);
        builder.Property(_ => _.Phone).IsRequired().HasMaxLength(30).IsUnicode(false);

        builder.HasMany(e => e.Orders)
                      .WithOne(e => e.Customer)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(_ => new { _.Email }).IsUnique();
    }
}
