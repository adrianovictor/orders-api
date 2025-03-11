using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Queries.Products.GetById;

public class GetProductByIdHandler(IProductReadRepository produrectRepository) : IRequestHandler<GetProductByIdCommand, ProductDto>
{
    private readonly IProductReadRepository _productRepository = produrectRepository;

    public async Task<ProductDto> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new ApplicationException($"Product com Id {request.ProductId} não encontrado.");
        }

        return product;
    }
}
