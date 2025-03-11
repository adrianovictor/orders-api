using MediatR;

namespace OrdersService.Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
}
