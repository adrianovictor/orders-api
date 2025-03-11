using MediatR;

namespace OrdersService.Application.Commands.Products.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public int ProductId { get; set; }
}
