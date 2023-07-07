using IdentityServer.Service.Dtos;
using IdentityServer.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceShared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer.Service.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var  user = new ApplicationUser 
            {
                UserName = signUpDto.UserName, 
                Email = signUpDto.Email ,
                City = signUpDto.City ,
            };
            var result = await _userManager.CreateAsync(user,signUpDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(ResponseDto<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(),400));
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim= User.Claims.FirstOrDefault(a=>a.Type==JwtRegisteredClaimNames.Sub);
            if (userIdClaim==null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user==null)
            {
                return BadRequest();
            }
            return Ok(new {Id=user.Id,UserName=user.UserName,Email=user.Email,City=user.City});

        }
    }
}
