using OrdersService.Domain.Core;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Entities;

public class OrderItem : Entity<OrderItem>
{
    public int OrderId { get; protected set; }
    public virtual Order Order{ get; protected set; }
    public int ProductId { get; protected set; }
    public virtual Product Product{ get; protected set; }
    public string ProductName { get; protected set; }
    public int Quantity { get; protected set; } 
    public decimal UnitPrice { get; protected set; }
    public decimal TotalPrice { get; protected set; }

    protected OrderItem() { }

    public OrderItem(Order order, Product product, int quantity, decimal unitPrice) 
    {
        if (order == null)
            throw new DomainException("O pedido não pode ser nulo.");

        if (product == null)
            throw new DomainException("O produto não pode ser nulo.");
            
        OrderId = order.Id;
        Order = order;
        Product = product;
        ProductId = product.Id;
        ProductName = product.Name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = UnitPrice * Quantity;
    }   

    public void UpdateQuantity(int quantity)
    {
        if (Quantity <= 0)
            throw new DomainException("A quantidade deve ser maior que zero.");
            
        if (UnitPrice <= 0)
            throw new DomainException("O preço unitário deve ser maior que zero.");

        Quantity = quantity;
        TotalPrice = UnitPrice * Quantity;    
    }
  
}
