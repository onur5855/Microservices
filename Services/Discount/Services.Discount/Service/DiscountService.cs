using Dapper;
using Npgsql;
using ServiceShared.Dtos;
using System.Data;

namespace Services.Discount.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbconnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbconnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            var status = await _dbconnection.ExecuteAsync("delete from discount where id=@Id",new {Id=id});
            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("discount Not Found ",404);
        }
        public async Task<ResponseDto<List<Model.Discount>>> GetAll()
        {
            var discount = await _dbconnection.QueryAsync<Model.Discount>("Select * from discount");
            return  ResponseDto<List<Model.Discount>>.Success(discount.ToList(),200);
        }
        public async Task<ResponseDto<Model.Discount>> GetByCodeUserId(string code, string userId)
        {

            var discount = await _dbconnection.QueryAsync<Model.Discount>("select * from discount where userid=@UserId and code=@Code",
                new {UserId=userId,Code=code});
            var hasdiscount = discount.FirstOrDefault();
            if (hasdiscount == null)
            {
                return ResponseDto<Model.Discount>.Fail("discount not found", 404);
            }
            return  ResponseDto<Model.Discount>.Success(hasdiscount,200); 


        }

        public async Task<ResponseDto<Model.Discount>> GetById(int Id)
        {
            var discount = (await _dbconnection.QueryAsync<Model.Discount>("Select * from discount where id =@id", new {id=Id})).SingleOrDefault();
            if (discount == null)
            {
                return ResponseDto<Model.Discount>.Fail("Dsicount Not Found",404);
            }
            return ResponseDto<Model.Discount>.Success(discount,200);

        }

        public async Task<ResponseDto<NoContent>> Save(Model.Discount discount)
        {
            var saveStatus = await _dbconnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)",discount);
            if (saveStatus>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            return ResponseDto<NoContent>.Fail("an error accourd while adding",500);
        }

        public async Task<ResponseDto<NoContent>> Update(Model.Discount discount)
        {
            var status = await _dbconnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id",
                new { Id=discount.Id,UserId=discount.UserId,Rate=discount.Rate,Code=discount.Code});
            if (status>0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            return ResponseDto<NoContent>.Fail("discount not Found",404);
        }
    }
}
