using MediatR;
using OrdersService.Domain.Models;
using OrdersService.Domain.Interfaces.Repository.Reading;

namespace OrdersService.Application.Queries.Customers.GetAll;

public class GetAllCustomersHandler(ICustomerReadRepository customerReadRepository) : IRequestHandler<GetAllCustomersCommand, IEnumerable<CustomerDto>>
{
    private readonly ICustomerReadRepository _customerReadRepository = customerReadRepository;

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersCommand request, CancellationToken cancellationToken)
    {
        var customers = await _customerReadRepository.GetAllAsync();

        return customers.Select(_ => new CustomerDto {
            Id = _.Id,
            Name = _.Name,
            Email = _.Email,
            Phone = _.Phone
        });
    }
}
