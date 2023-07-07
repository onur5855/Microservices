using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PhotoStock.Dtos;
using ServiceShared.ControllerBases;
using ServiceShared.Dtos;

namespace Service.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustumBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave( IFormFile image , CancellationToken cancellationToken)
        {
            if (image != null && image.Length>0)
            {
                var url = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", image.FileName);
                using (var stream = new FileStream(url,FileMode.Create))
                {
                    await image.CopyToAsync(stream,cancellationToken);
                }
                var returnurl= image.FileName;

                PhotoDto photoDto = new() { Url= returnurl };
                return CreateActionResultIntance(ResponseDto<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultIntance(ResponseDto<PhotoDto>.Fail("Photo is Empty", 400));
        }


        public IActionResult PhotoDelete(string ımageUrl)
        {
            var url = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", ımageUrl);
            if (!System.IO.File.Exists(url))
            {
                return CreateActionResultIntance(ResponseDto<NoContent>.Fail("photo Not Found", 404));
            }
            System.IO.File.Delete(url);
            return CreateActionResultIntance(ResponseDto<NoContent>.Success(204));

        }
    }
}
