using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;

namespace OrdersService.Application.Commands.Products.DeleteProduct;

public class DeleteProductHandler(IUnitOfWork unitOfWork,
    IProductWriteRepository productWriteRepository,
    IProductReadRepository productReadRepository) : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductWriteRepository _productWriteRepository = productWriteRepository;
    private readonly IProductReadRepository _productReadRepository = productReadRepository;

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productWrite = await _productWriteRepository.GetByIdAsync(request.ProductId);
        if (productWrite == null)
        {
            throw new ArgumentNullException($"Não foi possível localizar o produto com Id {request.ProductId}");
        }

        _productWriteRepository.Remove(productWrite);
        await _unitOfWork.CommitAsync();

        var productRead = await _productReadRepository.GetByIdAsync(request.ProductId);
        if (productRead != null)
        {
            await _productReadRepository.DeleteAsync(request.ProductId);
        }

        return new Unit { };
    }

}
