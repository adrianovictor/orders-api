using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Orders.GetByCustomerId;

public class GetOrderByCustomerIdCommand : IRequest<IEnumerable<OrderDto>>
{
    public int CustomerId { get; set; }
}
