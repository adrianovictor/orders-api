using MediatR;
using OrdersService.Domain.Interfaces.Repository.Reading;
using OrdersService.Domain.Interfaces.Repository.Writing;
using OrdersService.Domain.Interfaces.UoW;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Commands.Products.UpdateProduct;

public class UpdateProductHandler(IUnitOfWork unitOfWork,
    IProductWriteRepository customerWrite,
    IProductReadRepository customerRead) : IRequestHandler<UpdateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductWriteRepository _customerWrite = customerWrite;
    private readonly IProductReadRepository _customerRead = customerRead;

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productWrite = await _customerWrite.GetByIdAsync(request.Id);
        if (productWrite == null)
        {
            throw new ArgumentNullException($"Não foi possível localizar o produto com Id {request.Id}");
        }

        productWrite.ChangeName(request.Name);
        productWrite.ChangePrice(request.Price);

        _customerWrite.Update(productWrite);
        await _unitOfWork.CommitAsync();

        var productRead = await _customerRead.GetByIdAsync(request.Id);
        if (productRead == null)
        {
            await _customerRead.AddAsync(new ProductDto
            {
                Id = productWrite.Id,
                Name = productWrite.Name,
                Price = productWrite.Price
            });
        }
        else
        {
            productRead.Name = request.Name;
            productRead.Price = request.Price;

            await _customerRead.UpdateAsync(productRead);
        }

        return productWrite.Id;
    }
}
