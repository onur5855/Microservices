using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.ConfigurationSettings;
using Services.Catalog.Dtos;
using Services.Catalog.Model;
using ServiceShared.Dtos;

namespace Services.Catalog.Services
{
    public class CatagoryService : ICatagoryService
    {
        private readonly IMongoCollection<Category> _categorisCollection;
        private readonly IMapper _mapper;

        public CatagoryService( IMapper mapper,IDataBaseSettings dataBaseSettings)
        {
            var client=new MongoClient(dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(dataBaseSettings.DataBaseName);

            _categorisCollection = database.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categorisCollection.Find(category => true).ToListAsync();
            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categorisCollection.InsertOneAsync(category);
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
        }
        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string Id)
        {
            var category=await _categorisCollection.Find<Category>(x=>x.Id==Id).FirstOrDefaultAsync();
            if (category==null)
            {
                return ResponseDto<CategoryDto>.Fail("Category Not Found",404);
            }
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

    }
}
