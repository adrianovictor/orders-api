using System.ComponentModel.DataAnnotations;
using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Entities;

public class Customer : Entity<Customer>
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string Phone { get; protected set; }
    public virtual ICollection<Order> Orders { get; protected set; } = [];

    protected Customer() { }

    /// <summary>
    /// Construtor para a classe Customer
    /// </summary>
    /// <param name="name">Nome do cliente</param>
    /// <param name="email">Endereço de email do cliente</param>
    /// <param name="phone">Telefone do cliente</param>
    /// <exception cref="DomainException">Retorna uma exceção caso haja algum problema com os dados</exception>
    public Customer(string name, string email, string phone)
    {
        name.ThrowIfNullOrWhiteSpace(nameof(name), "O nome não pode ser vazio.");
        email.ThrowIfNullOrWhiteSpace(nameof(email), "O endereço de e-mail não pode ser vazio.");
        if (!ValidateEmail(email))
        {
            throw new DomainException("O endereço de e-mail fornecido não é válido.");
        }
        phone.ThrowIfNullOrWhiteSpace(nameof(phone), "O telefone não pode ser vazio.");

        Name = name;
        Email = email;
        Phone = phone;
    }

    /// <summary>
    /// Método estático para criar uma instância de Customer
    /// </summary>
    /// <param name="name">Nome do cliente</param>
    /// <param name="email">Endereço de email do cliente</param>
    /// <param name="phone"></param>
    /// <returns></returns>
    public static Customer Create(string name, string email, string phone)
    {
        return new(name, email, phone);
    }

    /// <summary>
    /// Altera o nome do cliente
    /// </summary>
    /// <param name="name"></param>
    public void ChangeName(string name)
    {
        name.ThrowIfNullOrWhiteSpace(nameof(name), "O nome não pode ser vazio.");

        Name = name;
    }

    /// <summary>
    /// Altera o endereço de email do cliente
    /// </summary>
    /// <param name="email">novo endereço de email</param>
    /// <exception cref="DomainException">retorna uma exceção</exception>
    public void ChangeEmail(string email)
    {
        email.ThrowIfNullOrWhiteSpace(nameof(email), "O endereço de e-mail não pode ser vazio.");
        if (!ValidateEmail(email))
        {
            throw new DomainException("O endereço de e-mail fornecido não é válido.");
        }

        Email = email;
    }

    /// <summary>
    /// Altera o número de telefone do cliente
    /// </summary>
    /// <param name="phone">novo número de telefone</param>
    public void ChangePhone(string phone)
    {
        phone.ThrowIfNullOrWhiteSpace(nameof(phone), "O telefone não pode ser vazio.");

        Phone = phone;
    }

    /* Valida o email informado */
    private bool ValidateEmail(string email)
    {
        var emailValidator = new EmailAddressAttribute();
        return emailValidator.IsValid(email);
    }
}
