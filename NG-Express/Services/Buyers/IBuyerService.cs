using NG_Express.Responses;
using NG_Express.Models;

namespace NG_Express.Services.Buyers
{
    public interface IBuyerService
    {
        Task<LoginResponse> Login(string Username, string Password);
        Task<RegisterResponse> Register(Buyer buyers);
        Task<Buyer> GetBuyerByIdAsync(int Id);
    }
}
