﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using Web.Models.Basket;
using Web.Models.Discounts;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseId(courseId);


            var basketItem = new BasketItemViewModel
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Price = course.Price
            };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
        {
            var discountStatus = await _basketService.ApplyDiscount(discountApplyInput.Code);
            TempData["discountStatus"]=discountStatus;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CancelApplyDiscount()
        {
            await _basketService.CancelApplyDiscount();
            return RedirectToAction(nameof(Index)); 
        }

    }
}
