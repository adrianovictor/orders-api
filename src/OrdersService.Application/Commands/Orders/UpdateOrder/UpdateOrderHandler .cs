using MediatR;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Enum;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Commands.Orders.UpdateOrder;

public class UpdateOrderHandler(IOrderWriteRepository orderWriteRepository, 
    IProductWriteRepository productWriteRepository, 
    ICustomerWriteRepository customerWriteRepository, 
    IUnitOfWork unitOfWork, 
    IOrderReadRepository orderReadRepository) : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderWriteRepository _orderRepository = orderWriteRepository;
    private readonly IProductWriteRepository _productRepository = productWriteRepository;
    private readonly ICustomerWriteRepository _customerRepository = customerWriteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(request.OrderId);
        if (order == null)
            throw new ApplicationException("Pedido não encontrado");

        // Se o pedido já foi enviado ou entregue, não pode ser atualizado
        if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
            throw new ApplicationException("Pedido já enviado ou entregue não pode ser alterado");

        var customer = await _customerRepository.GetByIdAsync(order.CustomerId);
        var originalItems = order.Items.ToList();

        foreach (var item in originalItems)
        {
            order.RemoveItem(item.ProductId);
        }

        // Atualiza o status do pedido
        order.ChangeStatus((OrderStatus)request.OrderStatus);

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _productRepository.GetProductsByIdsAsync(productIds);

        // Adicionar os novos itens ao pedido
        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);

            // Adicionar item ao pedido
            order.AddItem(order, product, product.Price, item.Quantity);

            // Atualizar produto no repositório
            _productRepository.Update(product);
        }

        _orderRepository.Update(order);
        await _unitOfWork.CommitAsync();

        // Atualizar o repositório de leitura
        await _orderReadRepository.UpdateAsync(MapToOrderDto(order, customer.Name));

        return true;
    }

    private OrderDto MapToOrderDto(Order order, string customerName)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            OrderStatus = (int)order.Status,
            TotalAmount = order.TotalAmount,
            Items = order.Items.Select(i => new OrderItemDto
            (
                Id: i.Id,
                OrderId: order.Id,
                ProductId: i.ProductId,
                ProductName: i.ProductName,
                UnitPrice: i.UnitPrice,
                Quantity: i.Quantity,
                TotalPrice: i.TotalPrice
            )).ToList()
        };
    }
}
