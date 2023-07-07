using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ServiceShared.Service
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //public string UserId => _httpContextAccessor.HttpContext.User.Claims.Where(a=>a.Type=="sub").FirstOrDefault().Value;
        public string UserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
        //public string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue("sub");
    }
}
