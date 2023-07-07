using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Protocol.Core.Types;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _ıdentityService;

        public AuthController(IIdentityService ıdentityService)
        {
            _ıdentityService = ıdentityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult > SignIn(SignInInput signInInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var responses = await _ıdentityService.SignIn(signInInput);
            if (!responses.IsSuccessFul)
            {
                responses.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(string.Empty, x);
                });
                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _ıdentityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index),"Home");

        }





    }
}
