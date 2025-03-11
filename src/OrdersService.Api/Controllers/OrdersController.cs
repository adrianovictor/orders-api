using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Commands.Orders.CreateOrder;
using OrdersService.Application.Commands.Orders.UpdateOrder;
using OrdersService.Application.Queries.Orders.GetAll;
using OrdersService.Application.Queries.Orders.GetByCustomerId;
using OrdersService.Application.Queries.Orders.GetById;
using OrdersService.Domain.Models;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]        
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllOrdersCommand());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]        
        public async Task<ActionResult<OrderDto>> Get_ById(int id)
        {
            var query = new GetOrderByIdCommand { OrderId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]        
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get_ByCustomerId(int customerId)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrderByCustomerIdCommand { CustomerId = customerId});
                return Ok(orders);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderCommand command)
        {
            try
            {
                var orderId = await _mediator.Send(command);
                return Ok(orderId);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderCommand command)
        {
            try
            {
                command.OrderId = id;

                var result = await _mediator.Send(command);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
