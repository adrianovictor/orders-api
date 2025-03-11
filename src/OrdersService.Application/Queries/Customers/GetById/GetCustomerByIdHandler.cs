using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Customers.GetById;

public class GetCustomerByIdHandler(ICustomerReadRepository customerReadRepository) : IRequestHandler<GetCustomerByIdCommand, CustomerDto>
{
    private readonly ICustomerReadRepository _customerReadRepository = customerReadRepository;

    public async Task<CustomerDto> Handle(GetCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
        {
            throw new ApplicationException($"Cliente com Id {request.CustomerId} não encontrado.");
        }

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone
        };
    }
}
