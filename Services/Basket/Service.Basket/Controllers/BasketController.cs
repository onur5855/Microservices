using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Basket.Dtos;
using Service.Basket.Services;
using ServiceShared.ControllerBases;
using ServiceShared.Dtos;
using ServiceShared.Service;

namespace Service.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustumBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultIntance(await _basketService.GetBasket(_sharedIdentityService.UserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            basketDto.UserId = _sharedIdentityService.UserId;
            var response = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResultIntance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultIntance(await _basketService.Delete(_sharedIdentityService.UserId));
        }








    }
}
