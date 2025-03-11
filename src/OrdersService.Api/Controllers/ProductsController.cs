using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Commands.Products.CreateProducts;
using OrdersService.Application.Commands.Products.DeleteProduct;
using OrdersService.Application.Commands.Products.UpdateProduct;
using OrdersService.Application.Queries.Products.GetAll;
using OrdersService.Application.Queries.Products.GetById;
using OrdersService.Domain.Models;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post(CreateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllProductsCommand());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> Get_ById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdCommand { ProductId = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateProductCommand request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand
            {
                ProductId = id,
            });
            return Ok(result);
        }
    }
}
