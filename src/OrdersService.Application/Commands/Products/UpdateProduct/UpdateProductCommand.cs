using MediatR;

namespace OrdersService.Application.Commands.Products.UpdateProduct;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
