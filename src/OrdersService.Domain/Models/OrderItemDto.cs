namespace OrdersService.Domain.Models;

public record class OrderItemDto(int Id, int OrderId, int ProductId, string ProductName, int Quantity, decimal UnitPrice, decimal TotalPrice);