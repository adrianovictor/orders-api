namespace OrdersService.Domain.Exceptions;

public class DomainValidationException : Exception
{
    public Dictionary<string, string> Errors { get; }

    public DomainValidationException(Dictionary<string, string> errors)
        : base("Erro de validação nos campos.")
    {
        Errors = errors;
    }
}