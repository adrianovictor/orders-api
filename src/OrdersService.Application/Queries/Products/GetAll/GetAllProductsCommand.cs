using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Products.GetAll;

public class GetAllProductsCommand : IRequest<IEnumerable<ProductDto>>
{
}
