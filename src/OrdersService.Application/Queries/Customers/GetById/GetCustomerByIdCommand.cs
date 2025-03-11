using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Customers.GetById;

public class GetCustomerByIdCommand : IRequest<CustomerDto>
{
    public int CustomerId { get; set; }
}
