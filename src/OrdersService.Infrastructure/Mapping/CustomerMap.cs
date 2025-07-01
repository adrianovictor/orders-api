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
        builder.OwnsOne(p => p.Email, email =>
        {
            email.Property(e => e.Address).HasColumnName("Email").HasMaxLength(255).IsRequired().IsUnicode(false);
        });        
        builder.Property(_ => _.Phone).IsRequired().HasMaxLength(30).IsUnicode(false);

        builder.HasMany(e => e.Orders)
                      .WithOne(e => e.Customer)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(_ => new { _.Email }).IsUnique();
    }
}
