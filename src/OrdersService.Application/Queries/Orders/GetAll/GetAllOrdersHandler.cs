using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Orders.GetAll;

public class GetAllOrdersHandler(IOrderReadRepository orderReadRepository,
    ICustomerReadRepository customerReadRepository) : IRequestHandler<GetAllOrdersCommand, IEnumerable<OrderResponseDto>>
{
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;
    private readonly ICustomerReadRepository _customerReadRepository = customerReadRepository;

    public async Task<IEnumerable<OrderResponseDto>> Handle(GetAllOrdersCommand request, CancellationToken cancellationToken)
    {
        var orders = await _orderReadRepository.GetAllAsync();
        var customerIds = orders.Select(_ => _.CustomerId).ToList();

        var customers = await _customerReadRepository.GetCustomersByIdsAsync(customerIds);

        if (orders.Any())
        {
            return orders.Select(x => new OrderResponseDto 
            { 
                Id = x.Id,
                CustomerId = x.CustomerId,
                CustomerName = customers.FirstOrDefault(_ => _.Id == x.CustomerId)!.Name,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                OrderStatus = x.OrderStatus,
                Items = x.Items
            }).ToList();
        }

        return [new OrderResponseDto()];
    }
}
