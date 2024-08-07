﻿using NG_Express.Responses;
using NG_Express.Models;
using NG_Express.Security;
using Data;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;
namespace NG_Express.Services.Buyers
{
    public class BuyerService : IBuyerService
    {
        private readonly AppDbContext _db;
        private readonly AuthToken _authToken;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthStateProvider _stateProvider;
        public BuyerService(AppDbContext db,AuthToken authToken,ILocalStorageService localStorageService,AuthStateProvider authStateProvider)
        {
            _db = db;
            _authToken = authToken;
            _localStorageService = localStorageService;
            _stateProvider = authStateProvider;
        }
        public async Task<Buyer> GetBuyerByIdAsync(int Id)
        {
            var Buyer = _db.Buyers.FirstOrDefault(x => x.Id == Id);
            return Buyer;
        }
        public async Task<BuyerLoginResponse> LoginAsync(string Username, string Password)
        {
            try
            {
                var user = await _db.Buyers
                                    .FirstOrDefaultAsync(b => b.Username == Username);
                if (user == null) return new BuyerLoginResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.NotFound,
                    Message = "Username is Incorrect"
                };
                if (user.PasswordHash != Password) return new BuyerLoginResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.NotFound,
                    Message = "Password is Incorrect"
                };
                var token = _authToken.GenerateToken(user,"Buyer");
                await _localStorageService.SetItemAsync("auth", token);
                _stateProvider.MarkUserAsAuthenticated(token);
                return new BuyerLoginResponse
                {
                    buyer = user,
                    Status = (int)System.Net.HttpStatusCode.Accepted,
                    Message = "you Logined successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new BuyerLoginResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.BadGateway,
                    Message = ex.Message
                };
            }

        }

        public async Task<BuyerRegisterResponse> Register(Buyer buyers)
        {
            try
            {
                var unique = await _db.Buyers
                                .FirstOrDefaultAsync(a => a.Username == buyers.Username);
                if (unique != null)
                {
                    return new BuyerRegisterResponse
                    {
                        buyer = new Buyer(),
                        Status = (int)System.Net.HttpStatusCode.BadRequest,
                        Message = "User in this username is created"
                    };
                }
                _db.Buyers.Add(buyers);
                await _db.SaveChangesAsync();
                return new BuyerRegisterResponse
                {
                    buyer = buyers,
                    Status = (int)System.Net.HttpStatusCode.Created,
                    Message = "Buyer Created Successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new BuyerRegisterResponse
                {
                    buyer = new Buyer(),
                    Status = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
