using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;
using OrdersService.Domain.ValueObjects;

namespace OrdersService.Domain.Entities;

public class Customer : Entity<Customer>
{
    public string Name { get; protected set; }
    public Email Email { get; protected set; }
    public string Phone { get; protected set; }
    public virtual ICollection<Order> Orders{ get; protected set; } = [];

    protected Customer() { }

    public Customer(string name, Email email, string phone)
    {
        name.ThrowIfNullOrWhiteSpace(nameof(name), "O nome não pode ser vazio.");
        email.ThrowIfNull(nameof(email), "Email não pode ser nulo.");
        phone.ThrowIfNullOrWhiteSpace(nameof(phone), "O telefone não pode ser vazio.");

        Name = name;
        Email = email;
        Phone = phone;
    }

    public static Customer Create(string name, Email email, string phone)
    {
        return new(name, email, phone);
    }

    public void ChangeName(string name)
    {
        name.ThrowIfNullOrWhiteSpace(nameof(name), "O nome não pode ser vazio.");

        Name = name;
    }

    public void ChangeEmail(Email email)
    {
        email.ThrowIfNull(nameof(email), "Email não pode ser nulo.");

        Email = email;

        Email = email;
    }

    public void ChangePhone(string phone)
    {
        phone.ThrowIfNullOrWhiteSpace(nameof(phone), "O telefone não pode ser vazio.");

        Phone = phone;
    }
}
