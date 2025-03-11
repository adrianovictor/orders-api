using MediatR;

namespace OrdersService.Application.Services;

public class SyncDataNotification(string message) : INotification
{
    public string Message { get; set; } = message;
}
