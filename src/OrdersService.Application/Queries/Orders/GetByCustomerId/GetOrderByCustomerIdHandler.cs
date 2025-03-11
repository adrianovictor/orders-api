using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Orders.GetByCustomerId;

public class GetOrderByCustomerIdHandler(IOrderReadRepository orderRepository) : IRequestHandler<GetOrderByCustomerIdCommand, IEnumerable<OrderDto>>
{
    private readonly IOrderReadRepository _orderReadRepository = orderRepository;

    public async Task<IEnumerable<OrderDto>> Handle(GetOrderByCustomerIdCommand request, CancellationToken cancellationToken)
    {
        return await _orderReadRepository.GetOrdersByCustomerIdAsync(request.CustomerId);
    }
}
