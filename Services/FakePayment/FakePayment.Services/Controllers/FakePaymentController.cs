using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceShared.ControllerBases;
using ServiceShared.Dtos;

namespace FakePayment.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustumBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultIntance(ResponseDto<ServiceShared.Dtos.NoContent>.Success(200));
        }


    }
}
