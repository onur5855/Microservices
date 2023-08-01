﻿using ServiceShared.Dtos;
using Web.Models.Discounts;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            var response = await _httpClient.GetAsync($"Discount/GetByCode/{discountCode}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var discount = await response.Content.ReadFromJsonAsync<ResponseDto<DiscountViewModel>>();
            return discount.Data;

        }
    }
}
