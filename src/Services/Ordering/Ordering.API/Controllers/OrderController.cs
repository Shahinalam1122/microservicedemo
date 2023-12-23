using CoreApiResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using Ordering.Domain.Models;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserName(string userName)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrdersByUserQuery(userName));
                return CustomResult("Order Load Successfully...",orders);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand orderCommand)
        {
            try
            {
                var isOrderPlaced = await _mediator.Send(orderCommand);
                if(isOrderPlaced)
                {
                    return CustomResult("Order has been placed...");
                }
                return CustomResult("Order not placed...",HttpStatusCode.BadRequest);
                
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand orderCommand)
        {
            try
            {
                var isModified = await _mediator.Send(orderCommand);
                if (isModified)
                {
                    return CustomResult("Order has been Modified...");
                }
                return CustomResult("Order modified failed...", HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var isDeleted = await _mediator.Send(new DeleteOrderCommand() { Id=orderId });
                if (isDeleted)
                {
                    return CustomResult("Order has been Deleted...");
                }
                return CustomResult("Order Deleted failed...", HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
