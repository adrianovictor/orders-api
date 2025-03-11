using MediatR;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Commands.Products.CreateProducts;

public class CreateProductHandler(IUnitOfWork unitOfWork, 
    IProductWriteRepository productWrite, 
    IProductReadRepository productRead) : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductWriteRepository _productWriteRepository = productWrite;
    private readonly IProductReadRepository _productReadRepository = productRead;

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, request.Price);

        await _productWriteRepository.AddAsync(product);
        await _unitOfWork.CommitAsync();

        await _productReadRepository.AddAsync(new ProductDto
        (
            Id: product.Id,
            Name: product.Name,
            Price: product.Price
        ));

        return product.Id;
    }
}
