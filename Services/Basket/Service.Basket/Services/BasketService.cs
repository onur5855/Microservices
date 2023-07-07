using Service.Basket.Dtos;
using ServiceShared.Dtos;
using System.Text.Json;

namespace Service.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly  RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var status=await _redisService.GetDatabase().KeyDeleteAsync(userId);
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket Not Found", 404);
        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string UserId)
        {
            var existBasket= await _redisService.GetDatabase().StringGetAsync(UserId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return ResponseDto<BasketDto>.Fail("Basket Not Found", 404);
            }
            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<ResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket could  not Update or Save",500);
        }
    }
}
