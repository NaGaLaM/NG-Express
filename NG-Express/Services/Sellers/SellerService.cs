using Blazored.LocalStorage;
using Data;
using NG_Express.Models;
using NG_Express.Responses;
using NG_Express.Security;
namespace NG_Express.Services.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly AppDbContext _db;
        private readonly AuthStateProvider _authStateProvider;
        private readonly AuthToken _authToken;
        private readonly ILocalStorageService _localStorageService;
        public SellerService(AppDbContext db,AuthStateProvider authStateProvider,AuthToken authToken,ILocalStorageService localStorageService) 
        {
            _db = db;
            _authStateProvider = authStateProvider;
            _authToken = authToken;
            _localStorageService = localStorageService;
        }
        public async Task<Seller?> GetSellerByIdAsync(int Id)
        {
            Seller? seller = _db.Sellers.FirstOrDefault(s=>s.Id == Id);
            return seller;
        }

        public async Task<SellerRegisterResponse> RegisterAsync(Seller Seller)
        {
            var user = _db.Sellers.FirstOrDefault(c => c.Email == Seller.Email);
            if (user != null) return new SellerRegisterResponse
            {
                Seller = null,
                Message = "User in this email exist already",
                Status = (int)System.Net.HttpStatusCode.NotAcceptable,
            };
            _db.Sellers.Add(Seller);
            await _db.SaveChangesAsync();
            return new SellerRegisterResponse
            {
                Seller = Seller,
                Message = "User Created Sucessfully",
                Status = (int)System.Net.HttpStatusCode.OK,
            };
        }
        public async Task<SellerLoginResponse> LoginAsync(string Email,string Password)
        {
            var user = _db.Sellers.FirstOrDefault(s=> s.Email == Email);
            if (user == null) return new SellerLoginResponse
            {
                Seller = null,
                Message = "Email is incorrect",
                Status = (int)System.Net.HttpStatusCode.NotAcceptable
            };

            if (user.PasswordHash != Password) return new SellerLoginResponse
            {
                Seller = null,
                Message = "Password is incorrect",
                Status = (int)System.Net.HttpStatusCode.NotAcceptable
            };
            string Token = _authToken.GenerateToken(user,"Seller");
            _localStorageService.SetItemAsync("auth", Token);
            _authStateProvider.MarkUserAsAuthenticated(Token);
            return new SellerLoginResponse
            {
                Seller=user,
                Message = "Logined sucessfully",
                Status = (int)System.Net.HttpStatusCode.OK
            };


        }
    }
}
