﻿using System.Diagnostics.Eventing.Reader;
using Web.Models.Basket;

namespace Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);

        Task<BasketViewModel> Get();
        Task<bool> Delete();

        Task AddBasketItem(BasketItemViewModel basketItemViewModel);
        Task<bool> RemoveBasketItem(string courseId);
        Task<bool> ApplyDiscount(string discountCodu);
        Task<bool> CancelApplyDiscount();

    }
}
