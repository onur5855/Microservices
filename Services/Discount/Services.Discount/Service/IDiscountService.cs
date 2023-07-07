using Services.Discount.Model;
using ServiceShared.Dtos;

namespace Services.Discount.Service
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<Model.Discount>>> GetAll();
        Task<ResponseDto<Model.Discount>> GetById(int Id);
        Task<ResponseDto<NoContent>> Save(Model.Discount discount);
        Task<ResponseDto<NoContent>> Update(Model.Discount discount);
        Task<ResponseDto<NoContent>> Delete(int Id);

        Task<ResponseDto<Model.Discount>> GetByCodeUserId(string code, string userId);
    }
}
