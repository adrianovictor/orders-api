using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;

namespace OrdersService.Application.Commands.Customers.DeleteCustomer;

public class DeleteCustomerHandler(IUnitOfWork unitOfWork,
    ICustomerWriteRepository customerWrite,
    ICustomerReadRepository customerRead) : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICustomerWriteRepository _customerWriteRepository = customerWrite;
    private readonly ICustomerReadRepository _customerReadRepository = customerRead;

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerWrite = await _customerWriteRepository.GetByIdAsync(request.CustomerId);
        if (customerWrite == null)
        {
            throw new ArgumentNullException($"Não foi possível localizar o cliente com Id {request.CustomerId}");
        }

        _customerWriteRepository.Remove(customerWrite);
        await _unitOfWork.CommitAsync();

        var customerRead = await _customerReadRepository.GetByIdAsync(request.CustomerId);
        if (customerRead != null)
        {
            await _customerReadRepository.DeleteAsync(request.CustomerId);
        }

        return new Unit { };
    }
}
