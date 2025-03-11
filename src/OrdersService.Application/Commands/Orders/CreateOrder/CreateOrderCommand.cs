using MediatR;

namespace OrdersService.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public List<OrderItemCommand> Items { get; set; }
}
