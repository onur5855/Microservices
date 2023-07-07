using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Web.Exception;
using Web.Services.Interfaces;

namespace Web.Handler
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _ıdentityService;
        private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService ıdentityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _ıdentityService = ıdentityService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accesToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _ıdentityService.GetAccessTokenByRefreshToken();

                if (tokenResponse != null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                        ("Bearer", tokenResponse.AccessToken);
                     response = await base.SendAsync(request, cancellationToken);
                }

            }

            if (response.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }


            return response;

        }

    }
}
