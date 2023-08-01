using ServiceShared.Dtos;
using Web.Models.Basket;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {

            var basket = await Get();

            if (basket != null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                

                basket = new BasketViewModel();
                basket.BasketItems.Add(basketItemViewModel);
            }
            await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCodu)
        {
            await CancelApplyDiscount();
            var basket = await Get();
            if (basket == null )
            {
                return false;
            }
            var hasDiscount = await _discountService.GetDiscount(discountCodu);
            if (hasDiscount==null)
            {
                return false;
            }
            basket.DiscountRate = hasDiscount.Rate;
            basket.DiscountCode= hasDiscount.Code;
            await SaveOrUpdate(basket);
            return true;
        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await Get();
            if (basket==null)
            {
                return false;
            }
            basket.DiscountCode = null;
            await SaveOrUpdate(basket);
            return true;
        }

        public async Task<bool> Delete()
        {
            var result = await _httpClient.DeleteAsync("basket");
            return result.IsSuccessStatusCode;
        }

       

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();
            if (basket == null)
            {
                return false;
            }

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (deleteBasketItem == null)
            {
                return false;
            }
            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);
            if (!deleteResult)
            {
                return false;
            }
            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }
            return await SaveOrUpdate(basket);



        }
        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("basket");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<BasketViewModel>>();
            return basketViewModel.Data;
        }
        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            //http://localhost:5000/Services/Basket/
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("basket", basketViewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
