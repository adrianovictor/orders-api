using MediatR;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Products.GetById;

public class GetProductByIdCommand : IRequest<ProductDto>
{
    public int ProductId { get; set; }
}
