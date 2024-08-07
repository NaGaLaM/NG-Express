﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using NG_Express.Security;
namespace NG_Express.Security
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IJSRuntime _jsruntime;
        private readonly AuthToken _authToken;
        public AuthStateProvider(ILocalStorageService localStorageService,IJSRuntime jSRuntime, AuthToken authToken)
        {
            _localStorageService = localStorageService;
            _jsruntime = jSRuntime;
            _authToken = authToken;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
			var token = await _localStorageService.GetItemAsync<string>("auth");
            if (string.IsNullOrEmpty(token)) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            ClaimsPrincipal? user = _authToken.ValidateToken(token);
            if (user == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            return new AuthenticationState(user);
        }


        public void MarkUserAsAuthenticated(string token)
        {
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async void MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("auth");
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }



        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
