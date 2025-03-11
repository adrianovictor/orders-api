using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;

namespace OrdersService.Application.Commands.Orders.DeleteOrder;

public class DeleteOrderHandler(IUnitOfWork unitOfWork,
    IOrderWriteRepository orderWriteRepository,
    IOrderReadRepository orderReadRepository) : IRequestHandler<DeleteOrderCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderWriteRepository _orderWriteRepository = orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository = orderReadRepository;

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var productWrite = await _orderWriteRepository.GetByIdAsync(request.OrderId);
        if (productWrite == null)
        {
            throw new ArgumentNullException($"Não foi possível localizar o pedido com Id {request.OrderId}");
        }

        _orderWriteRepository.Remove(productWrite);
        await _unitOfWork.CommitAsync();

        var productRead = await _orderReadRepository.GetByIdAsync(request.OrderId);
        if (productRead != null)
        {
            await _orderReadRepository.RemoveAsync(request.OrderId);
        }

        return new Unit { };
    }
}
