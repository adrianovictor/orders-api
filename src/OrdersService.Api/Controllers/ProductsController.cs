using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Commands.Products.CreateProducts;
using OrdersService.Application.Queries.Products.GetAll;
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

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<ProductDto>> Get_ById(int id)
        //{
        //    var result = await _mediator.Send(new GetCustomerByIdCommand { CustomerId = id });
        //    return Ok(result);
        //}
    }
}
