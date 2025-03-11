using MediatR;

namespace OrdersService.Application.Commands.Products.CreateProducts;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
