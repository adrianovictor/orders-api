using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Domain.Models;
using OrdersService.Domain.ValueObjects;

namespace OrdersService.Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerHandler(IUnitOfWork unitOfWork, 
    ICustomerWriteRepository customerWrite, 
    ICustomerReadRepository customerRead) : IRequestHandler<UpdateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICustomerWriteRepository _customerWriteRepository = customerWrite;
    private readonly ICustomerReadRepository _customerReadRepository = customerRead;

    public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerWrite = await _customerWriteRepository.GetByIdAsync(request.CustomerId);
        if (customerWrite == null)
        {
            throw new ArgumentNullException($"Não foi possível localizar o cliente com Id {request.CustomerId}");
        }

        customerWrite.ChangeName(request.CustomerName);
        customerWrite.ChangeEmail(Email.Create(request.CustomerEmail));
        customerWrite.ChangePhone(request.CustomerPhone);

        _customerWriteRepository.Update(customerWrite);
        await _unitOfWork.CommitAsync();

        var customerRead = await _customerReadRepository.GetByIdAsync(request.CustomerId);
        if (customerRead == null)
        {
            await _customerReadRepository.AddAsync(new CustomerDto
            {
                Id = customerWrite.Id,
                Name = customerWrite.Name,
                Email = customerWrite.Email.Address,
                Phone = customerWrite.Phone
            });
        }
        else
        {
            customerRead.Name = request.CustomerName;
            customerRead.Email = request.CustomerEmail;
            customerRead.Phone = request.CustomerPhone;

            await _customerReadRepository.UpdateAsync(customerRead);
        }

        return customerWrite.Id;
    }
}
