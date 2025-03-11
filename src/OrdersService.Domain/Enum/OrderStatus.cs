namespace OrdersService.Domain.Enum;

public enum OrderStatus
{
    Created = 1,
    Processing,
    AwaitingPayment,
    Paid,
    Picking,
    Shipped,
    Delivered,
    Cancelled,
    Returned,
    Expired
}
