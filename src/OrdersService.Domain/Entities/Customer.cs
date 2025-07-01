using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;
using OrdersService.Domain.Validators;

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
        name.ThrowIfNullOrWhiteSpace(nameof(name), "O nome não pode ser vazio.");
        email.ThrowIfNullOrWhiteSpace(nameof(email), "O endereço de e-mail não pode ser vazio.");
        if (!EmailValidator.Validate(email))
        {
            throw new ArgumentException("O endereço de e-mail fornecido não é válido.", nameof(email));
        }        
        phone.ThrowIfNullOrWhiteSpace(nameof(phone), "O telefone não pode ser vazio.");

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
        name.ThrowIfNullOrWhiteSpace(nameof(name), "O nome não pode ser vazio.");

        Name = name;
    }

    public void ChangeEmail(string email)
    {
        email.ThrowIfNullOrWhiteSpace(nameof(email), "O endereço de e-mail não pode ser vazio.");
        if (!EmailValidator.Validate(email))
        {
            throw new ArgumentException("O endereço de e-mail fornecido não é válido.", nameof(email));
        } 

        Email = email;
    }

    public void ChangePhone(string phone)
    {
        phone.ThrowIfNullOrWhiteSpace(nameof(phone), "O telefone não pode ser vazio.");

        Phone = phone;
    }
}
