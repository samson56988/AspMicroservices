using Dapper;
using DiscountGrpc.Entities;
using Npgsql;

namespace DiscountGrpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected =
                 await connection.ExecuteAsync
                    ("Insert Into Coupon(productname,description,amount)Values(@productname,@description,@amount)",
            new { productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true; 
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
              (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("Delete from Coupon where productname = @productname",
                new { productname = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("Select * from Coupon where productname = @productname", new { productname = productName });
            if(coupon == null)
                return new Coupon {ProductName = "No Discount", Amount = 0, Description="No Discount Desc"};
            return coupon;

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
              (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected =
                 await connection.ExecuteAsync
                    ("Update Coupon set productname = @productname,description=@description,amount=@amount where id = @id",
            new { productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount, id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    } 
}
