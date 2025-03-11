using MediatR;
using OrdersService.Domain.Models;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Domain.Entities;

namespace OrdersService.Application.Commands.Customers.CreateCustomer;

public class CreateCustomerHandler(IUnitOfWork unitOfWork, 
    ICustomerWriteRepository customerRepository, 
    ICustomerReadRepository customerReadRepository) : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICustomerWriteRepository _customerRepository = customerRepository;
    private readonly ICustomerReadRepository _customerReadRepository = customerReadRepository;

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByEmailAsync(request.Email);
        if (customer is not null) 
        {
            throw new ApplicationException("E-mail is already registered.");
        }

        customer = Customer.Create(request.Name, request.Email, request.Phone);

        await _customerRepository.AddAsync(customer);
        await _unitOfWork.CommitAsync();

        /* Replica os dados no banco MongoDb */
        await _customerReadRepository.AddAsync(new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone
        });

        return customer.Id;
    }
}
