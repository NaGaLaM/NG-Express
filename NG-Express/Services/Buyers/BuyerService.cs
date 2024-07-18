using NG_Express.Responses;
using NG_Express.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace NG_Express.Services.Buyers
{
    public class BuyerService : IBuyerService
    {
        private readonly AppDbContext DB;
        public BuyerService(AppDbContext db)
        {
            DB = db;
        }
        public async Task<LoginResponse> Login(string Username, string Password)
        {
            try
            {
                var user = await DB.Buyers
                                    .FirstOrDefaultAsync(b => b.Username == Username);
                if (user == null) return new LoginResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.NotFound,
                    Message = "Username is Incorrect"
                };
                if (user.PasswordHash != Password) return new LoginResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.NotFound,
                    Message = "Password is Incorrect"
                };
                return new LoginResponse
                {
                    buyer = user,
                    Status = (int)System.Net.HttpStatusCode.Accepted,
                    Message = "you Logined successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new LoginResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.BadGateway,
                    Message = ex.Message
                };
            }

        }

        public async Task<RegisterResponse> Register(Buyer buyers)
        {
            try
            {
                var unique = await DB.Buyers
                                .FirstOrDefaultAsync(a => a.Username == buyers.Username);
                if (unique != null)
                {
                    return new RegisterResponse
                    {
                        buyer = new Buyer(),
                        Status = (int)System.Net.HttpStatusCode.BadRequest,
                        Message = "User in this username is created"
                    };
                }
                DB.Buyers.Add(buyers);
                await DB.SaveChangesAsync();
                return new RegisterResponse
                {
                    buyer = buyers,
                    Status = (int)System.Net.HttpStatusCode.Created,
                    Message = "Buyer Created Successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RegisterResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
