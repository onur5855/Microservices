using Microsoft.AspNetCore.Mvc;
using ServiceShared.Dtos;
using Web.Helpers;
using Web.Models;
using Web.Models.Catalog;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelpers _photoHelpers;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService,PhotoHelpers photoHelpers)
        {
            _photoHelpers = photoHelpers;
            _httpClient = httpClient;
            _photoStockService = photoStockService;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var result = await _photoStockService.Upload(courseCreateInput.formFile);
            if (result != null)
            {
                courseCreateInput.Picture = result.Url;
            }


            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("course",courseCreateInput);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"course/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("category");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();

            return responseSucces.Data;
        }

        public async Task<List< CourseViewModel>> GetAllCourseAsync()
        {
            //http:localhost:5000/service/catalog/course
            var response = await _httpClient.GetAsync("course");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
            responseSucces.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelpers.PhotoStockUrl(x.Picture);
            });
            return responseSucces.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            // api /[controller] / GetAllByUserId /{ userId}
            var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseViewModel>>>();
            responseSucces.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelpers.PhotoStockUrl(x.Picture);
            });
            

            return responseSucces.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"course/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<ResponseDto<CourseViewModel>>();
            responseSucces.Data.StockPictureUrl = _photoHelpers.PhotoStockUrl(responseSucces.Data.Picture);
            return responseSucces.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var result = await _photoStockService.Upload(courseUpdateInput.formFile);
            if (result != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = result.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("course", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
