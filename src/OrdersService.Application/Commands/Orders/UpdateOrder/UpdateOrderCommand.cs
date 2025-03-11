using MediatR;
using OrdersService.Application.Commands.Orders.CreateOrder;

namespace OrdersService.Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }
    public int OrderStatus { get; set; }
    public List<OrderItemCommand> Items { get; set; }
}