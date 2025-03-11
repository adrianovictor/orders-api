namespace OrdersService.Application.Commands.Orders.CreateOrder;

public class OrderItemCommand
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
