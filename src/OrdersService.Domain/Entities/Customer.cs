using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Entities;

public class Customer : Entity<Customer>
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string Phone { get; protected set; }
    public virtual ICollection<Order> Orders{ get; protected set; } = [];

    protected Customer() { }

    public Customer(string name, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Customer name cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Customer email cannot be empty.");
            
        if (string.IsNullOrWhiteSpace(phone))
            throw new DomainException("Customer phone cannot be empty.");

        Name = name;
        Email = email;
        Phone = phone;
    }

    public static Customer Create(string name, string email, string phone)
    {
        return new(name, email, phone);
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Customer name cannot be empty.");

        Name = name;
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Customer email cannot be empty.");

        Email = email;
    }

    public void ChanagePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new DomainException("Customer phone cannot be empty.");

        Phone = phone;
    }
}
