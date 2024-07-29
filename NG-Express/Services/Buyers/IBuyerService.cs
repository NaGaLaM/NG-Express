using NG_Express.Responses;
using NG_Express.Models;

namespace NG_Express.Services.Buyers
{
    public interface IBuyerService
    {
        Task<BuyerLoginResponse> LoginAsync(string Username, string Password);
        Task<BuyerRegisterResponse> Register(Buyer buyers);
        Task<Buyer> GetBuyerByIdAsync(int Id);
    }
}
