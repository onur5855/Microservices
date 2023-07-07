using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.ConfigurationSettings;
using Services.Catalog.Dtos;
using Services.Catalog.Model;
using ServiceShared.Dtos;

namespace Services.Catalog.Services
{
    public class CourseService :ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper,IDataBaseSettings dataBaseSettings)
        {
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(dataBaseSettings.DataBaseName);

            _courseCollection = database.GetCollection<Course>(dataBaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course=>true).ToListAsync();
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category=await _categoryCollection.Find<Category>(x=>x.Id==item.CategorId).FirstAsync();
                }
            }
            else
            {
                courses=new List<Course>();
            }
            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string Id)
        {
            var course= await _courseCollection.Find(x=>x.Id==Id).FirstOrDefaultAsync();
            if (course==null)
            {
                return ResponseDto<CourseDto>.Fail("Course Not Found", 404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategorId).FirstAsync();
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var course = await _courseCollection.Find<Course>(x=>x.UserId==userId).ToListAsync();
            if (course.Any())
            {
                foreach (var item in course)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.Id == item.CategorId).FirstAsync();
                }
            }
            else
            {
                course = new List<Course>();
            }
            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(course), 200);
        }
        public async Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        public async Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse=_mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);
            if (result==null)
            {
                return ResponseDto<NoContent>.Fail("Course Not found", 404);
            }
            return ResponseDto<NoContent>.Success(204);

        }
        public async Task<ResponseDto<NoContent>> DeleteAsync(string Id)
        {
            var result = await _courseCollection.DeleteOneAsync(x=>x.Id==Id);
            if (result.DeletedCount>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else
            {
                return ResponseDto<NoContent>.Fail("Course Not Found", 404);
            }
        }



    }
}
