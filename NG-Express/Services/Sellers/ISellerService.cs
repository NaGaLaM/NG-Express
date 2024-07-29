using NG_Express.Models;
using NG_Express.Responses;
namespace NG_Express.Services.Sellers
{
    public interface ISellerService
    {
        Task<Seller> GetSellerByIdAsync(int Id);
        Task<SellerRegisterResponse> RegisterAsync(Seller seller);
        Task<SellerLoginResponse> LoginAsync(string Email, string Password);
    }
}
