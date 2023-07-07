using Services.Catalog.Dtos;
using Services.Catalog.Model;
using ServiceShared.Dtos;

namespace Services.Catalog.Services
{
    public interface ICourseService
    {
         Task<ResponseDto<List<CourseDto>>> GetAllAsync();
         Task<ResponseDto<CourseDto>> GetByIdAsync(string Id);
         Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId);
         Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
         Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
         Task<ResponseDto<NoContent>> DeleteAsync(string Id);
       
    }
}
