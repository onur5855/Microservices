using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Discount.Service;
using ServiceShared.ControllerBases;
using ServiceShared.Service;

namespace Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustumBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultIntance(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount=await _discountService.GetById(id);
            return CreateActionResultIntance(discount);
        }

        [HttpGet]
        [Route("[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.UserId;
            var discount = await _discountService.GetByCodeUserId(code,userId);
            return CreateActionResultIntance(discount);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Model.Discount discount)
        {
            return CreateActionResultIntance(await _discountService.Save(discount));
        }
        [HttpPut]
        public async Task<IActionResult> Update(Model.Discount discount)
        {
            return CreateActionResultIntance(await _discountService.Update(discount));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultIntance(await _discountService.Delete(id));
        }




















    }
}
