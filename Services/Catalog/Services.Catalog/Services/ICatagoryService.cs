using Services.Catalog.Dtos;
using Services.Catalog.Model;
using ServiceShared.Dtos;

namespace Services.Catalog.Services
{
    public interface ICatagoryService
    {

        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string Id);
    }
}
