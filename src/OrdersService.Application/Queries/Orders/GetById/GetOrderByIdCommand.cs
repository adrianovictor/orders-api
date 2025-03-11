using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Orders.GetById;

public class GetOrderByIdCommand : IRequest<OrderResponseDto>
{
    public int OrderId { get; set; }
}
