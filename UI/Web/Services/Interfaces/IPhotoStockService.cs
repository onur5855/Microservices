using Web.Models.PhotoStock;

namespace Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> Upload(IFormFile formFile);
        Task<bool> DeletePhoto(string photoUrl);
    }
}
