using System;

namespace OrdersService.Domain.Exceptions;

public class DomainException(string message) : Exception(message)
{
}
