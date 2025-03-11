using OrdersService.Domain.Core;
using OrdersService.Domain.Enum;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Entities;

public class Order : Entity<Order>
{
    public int CustomerId { get; protected set; }
    public virtual Customer Customer { get; protected set; }
    public DateTime OrderDate { get; protected set; }
    public decimal TotalAmount { get; protected set; }
    public OrderStatus Status { get; protected set; }
    public virtual ICollection<OrderItem> Items { get; protected set; } = [];

    protected Order() { }

    public Order(Customer customer, DateTime orderDate, decimal totalAmount, OrderStatus status = OrderStatus.Created)
    {
        Customer = customer;
        CustomerId = customer.Id;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        Status = status;
    }

    public static Order Create(Customer customer, DateTime orderDate, decimal totalAmount, OrderStatus status = OrderStatus.Created)
    {
        return new(customer, orderDate, totalAmount, status);
    }

    public void AddItem(Order oder, Product product, decimal unitPrice, int quantity)
    {
        var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
        
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            var item = new OrderItem(oder, product, quantity, unitPrice);
            Items.Add(item);
        }
        
        RecalculateTotalAmount();
    }

    public void UpdateItem(int productId, int quantity)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        
        if (item == null)
            throw new DomainException("Item not found in order.");
            
        if (quantity <= 0)
        {
            Items.Remove(item);
        }
        else
        {
            item.UpdateQuantity(quantity);
        }
        
        RecalculateTotalAmount();
    }

    public void RemoveItem(int productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        
        if (item == null)
            throw new DomainException("Item não encontrado no pedido.");
            
        Items.Remove(item);
        RecalculateTotalAmount();
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = Items.Sum(i => i.TotalPrice);
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
    }
}
