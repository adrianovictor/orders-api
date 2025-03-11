using MediatR;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Commands.Orders.CreateOrder;

public class CreateOrderHandler(IOrderWriteRepository orderWriteRepository,
    IProductWriteRepository productRepository,
    ICustomerWriteRepository customerWriteRepository,
    IUnitOfWork unitOfWork,
    IOrderReadRepository orderReadRepository) : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderWriteRepository _orderRepository = orderWriteRepository;
    private readonly IProductWriteRepository _productRepository = productRepository;
    private readonly ICustomerWriteRepository _customerRepository = customerWriteRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new ApplicationException("Cliente não encontrado");

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _productRepository.GetProductsByIdsAsync(productIds);

        if (products.Count() != productIds.Count)
            throw new ApplicationException("Um ou mais produtos não encontrados");

        var totalAmount = request.Items.Sum(_ => 
        {
            var amount = products.First(p => p.Id == _.ProductId).Price * _.Quantity;
            return amount;
        });

        var order = new Order(customer, DateTime.Now, totalAmount, Domain.Enum.OrderStatus.Created);
        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            
            // Verificar estoque
            //if (product.StockQuantity < item.Quantity)
            //    throw new ApplicationException($"Estoque insuficiente para o produto {product.Name}");
            
            // Remover do estoque
            //product.RemoveFromStock(item.Quantity);
            
            // Adicionar item ao pedido
            order.AddItem(order, product, product.Price, item.Quantity);
            
            // Atualizar produto no repositório
            //_productRepository.Update(product);
        }        

        await _orderRepository.AddAsync(order);    
        await _unitOfWork.CommitAsync();


        await _orderReadRepository.AddAsync(MapToOrderDto(order, customer.Name));
        return order.Id;
    }

    private OrderDto MapToOrderDto(Order order, string customerName)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderStatus = (int)order.Status,
            Items = [.. order.Items.Select(i => new OrderItemDto
            (
                Id: i.Id,
                OrderId: order.Id,
                ProductId: i.ProductId,
                ProductName: i.ProductName,
                UnitPrice: i.UnitPrice,
                Quantity: i.Quantity,
                TotalPrice: i.TotalPrice
            ))]
        };
    }
}
