using System.ComponentModel.DataAnnotations;
using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Entities;

public class Customer : Entity<Customer>
{
    [Required(ErrorMessage = "O nome não pode ser vazio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    public string Name { get; protected set; }

    [Required(ErrorMessage = "O endereço de e-mail não pode ser vazio.")]
    [EmailAddress(ErrorMessage = "O endereço de e-mail fornecido não é válido.")]
    public string Email { get; protected set; }

    [Required(ErrorMessage = "O telefone não pode ser vazio.")]
    [Phone(ErrorMessage = "O telefone fornecido não é válido.")]
    public string Phone { get; protected set; }

    public virtual ICollection<Order> Orders { get; protected set; } = [];

    protected Customer() { }

    public Customer(string name, string email, string phone)
    {
        Name = name;
        Email = email;
        Phone = phone;

        Validate();
    }

    public static Customer Create(string name, string email, string phone)
    {
        return new(name, email, phone);
    }

    public void ChangeName(string name)
    {
        Name = name;
        Validate();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        Validate();
    }

    public void ChangePhone(string phone)
    {
        Phone = phone;
        Validate();
    }

    /// <summary>
    /// Valida o objeto atual usando DataAnnotations.
    /// </summary>
    /// <exception cref="DomainException"></exception>
    private void Validate()
    {
        var validationContext = new ValidationContext(this);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
        {
            var errors = validationResults.Select(r => r.ErrorMessage).ToList();
            throw new DomainException("Erro de validação: " + string.Join(" | ", errors));
        }
    }
}
