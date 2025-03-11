using System;
using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Orders.GetAll;

public class GetAllOrdersCommand : IRequest<IEnumerable<OrderResponseDto>>
{

}
