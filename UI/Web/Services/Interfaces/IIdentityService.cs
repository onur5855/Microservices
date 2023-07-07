using IdentityModel.Client;
using ServiceShared.Dtos;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseDto<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();

    }
}
