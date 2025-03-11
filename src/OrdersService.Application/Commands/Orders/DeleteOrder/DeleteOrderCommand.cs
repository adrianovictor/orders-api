using MediatR;

namespace OrdersService.Application.Commands.Orders.DeleteOrder;

public class DeleteOrderCommand : IRequest
{
    public int OrderId { get; set; }
}
