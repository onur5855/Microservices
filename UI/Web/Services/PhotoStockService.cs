using ServiceShared.Dtos;
using Web.Models.PhotoStock;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync( $"photo?ımageUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> Upload(IFormFile formFile)
        {
            if (formFile==null || formFile.Length<0)
            {
                return null;
            }
            // 132412351235.jpg
            var randomName= $"{Guid.NewGuid().ToString()}{Path.GetExtension(formFile.FileName)}";

            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);

            var multiPartContent = new MultipartFormDataContent();            

            multiPartContent.Add(new ByteArrayContent(ms.ToArray()), "image", randomName);

            var response = await _httpClient.PostAsync("Photo", multiPartContent);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<ResponseDto<PhotoViewModel>>();
            return responseSucces.Data;



        }
    }
}
