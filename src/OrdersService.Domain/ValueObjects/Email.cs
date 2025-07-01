using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;
using OrdersService.Domain.Validators;

namespace OrdersService.Domain.ValueObjects;

public class Email : ValueObject<Email>
{
    public string Address { get; protected set; }

    public Email(string address)
    {
        address.ThrowIfNullOrWhiteSpace(nameof(address), "O endereço de e-mail não pode ser vazio.");

        if (!EmailValidator.Validate(address))
        {
            throw new ArgumentException("O endereço de e-mail fornecido não é válido.", nameof(address));
        }

        Address = address;
    }
    public static Email Create(string address)
    {
        return new Email(address);
    }
}
