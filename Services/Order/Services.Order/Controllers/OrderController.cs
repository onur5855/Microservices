using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Order.Application.Commands;
using Services.Order.Application.Queries;
using ServiceShared.ControllerBases;
using ServiceShared.Service;

namespace Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustumBaseController 
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrderByUserIdQuery { UserId=_sharedIdentityService.UserId});
            return CreateActionResultIntance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand )
        {
            var response=await _mediator.Send(createOrderCommand);
            return CreateActionResultIntance(response);
        }



    }
}
