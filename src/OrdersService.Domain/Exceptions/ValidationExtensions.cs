namespace OrdersService.Domain.Exceptions;

public static class ValidationExtensions
{
    public static void ThrowIfNullOrWhiteSpace(this string value, string parameterName, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    public static void ThrowIfNull<T>(this T? value, string paramName, string? message = null) where T : class
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser nulo.");
        }
    }

    public static void ThrowIfNull<T>(this T? value, string paramName, string? message = null) where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser nulo.");
        }
    }

    public static void ThrowIfZeroOrNegative(this int value, string paramName, string? message = null)
    {
        if (value <= 0)
        {
            throw new ArgumentException(paramName, message ?? $"O parâmetro '{paramName}' deve ser maior que zero.");
        }
    }

    public static void ThrowIfNegative(this decimal value, string paramName, string? message = null)
    {
        if (value < 0)
        {
            throw new ArgumentException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser negativo.");
        }
    }            
}