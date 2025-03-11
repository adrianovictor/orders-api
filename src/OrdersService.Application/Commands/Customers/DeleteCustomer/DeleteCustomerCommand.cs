using MediatR;

namespace OrdersService.Application.Commands.Customers.DeleteCustomer;

public class DeleteCustomerCommand : IRequest
{
    public int CustomerId { get; set; }
}
