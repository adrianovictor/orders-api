using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Entities;

public class Product : Entity<Product>
{
    public string Name {get; protected set; }
    public decimal Price { get; protected set; }
    public virtual ICollection<OrderItem> Items { get; protected set; } = [];

    protected Product() { }

    public Product(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");
            
        if (price <= 0)
            throw new DomainException("Product price must be greater than zero.");

        Name = name;
        Price = price;
    }

    public static Product Create(string name, decimal price)
    {
        return new(name, price);
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");

        Name = name;        
    }

    public void ChangePrice(decimal price)
    {
        if (price <= 0)
            throw new DomainException("Product price must be greater than zero.");

        Price = price;        
    }
}
