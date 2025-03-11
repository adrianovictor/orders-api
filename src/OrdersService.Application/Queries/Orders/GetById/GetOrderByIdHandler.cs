using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Orders.GetById;

public class GetOrderByIdHandler(IOrderReadRepository orderReadRepository,
    ICustomerReadRepository customerRepository) : IRequestHandler<GetOrderByIdCommand, OrderResponseDto>
{
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;
    private readonly ICustomerReadRepository _customerReadRepository = customerRepository;

    public async Task<OrderResponseDto> Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId);
        
        if (order is not null)
        {
            var customer = await _customerReadRepository.GetByIdAsync(order.CustomerId);

            return new OrderResponseDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = customer.Name,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                Items = order.Items
            };
        }


        return new OrderResponseDto();
    }
}
