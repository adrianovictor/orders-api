using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Customers.GetAll;

public class GetAllCustomersCommand : IRequest<IEnumerable<CustomerDto>>
{

}
