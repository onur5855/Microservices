using Service.Basket.Dtos;
using ServiceShared.Dtos;

namespace Service.Basket.Services
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string UserId);
        Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<ResponseDto<bool>> Delete(string userId);
    }
}
