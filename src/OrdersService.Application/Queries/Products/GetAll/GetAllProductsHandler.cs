using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Products.GetAll;

public class GetAllProductsHandler(IProductReadRepository productRepositry) : IRequestHandler<GetAllProductsCommand, IEnumerable<ProductDto>>
{
    private readonly IProductReadRepository _productRepositry = productRepositry;

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        return await _productRepositry.GetAllAsync();
    }
}
