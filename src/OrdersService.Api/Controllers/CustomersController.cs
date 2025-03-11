using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Commands.Customers.CreateCustomer;
using OrdersService.Application.Commands.Customers.DeleteCustomer;
using OrdersService.Application.Commands.Customers.UpdateCustomer;
using OrdersService.Application.Queries.Customers.GetAll;
using OrdersService.Application.Queries.Customers.GetById;
using OrdersService.Domain.Models;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post(CreateCustomerCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllCustomersCommand());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDto>> Get_ById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdCommand { CustomerId = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] CreateCustomerCommand request)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand 
            { 
                CustomerId = id, 
                CustomerName = request.Name,
                CustomerEmail = request.Email,
                CustomerPhone = request.Phone
            });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand
            {
                CustomerId = id,
            });
            return Ok(result);
        }
    }
}
