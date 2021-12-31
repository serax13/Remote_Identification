using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http;
using Identification.Models;
using System.Security.Claims;
using Identification.Panel.Services.Contracts;

namespace Identification.Panel.Data
{
    public class CustomAuthentificationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public IUserService _userService { get; set; }
        private readonly HttpClient _httpClient;
        public CustomAuthentificationStateProvider(ILocalStorageService localStorageService,
            IUserService userService,
            HttpClient httpClient)
        {
            //throw new Exception("CustomAuthenticationStateProviderException");
            _localStorageService = localStorageService;
            _userService = userService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("accessToken");
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");

            ClaimsIdentity identity;

            if (accessToken != null && accessToken != string.Empty)
            {
                Users user = await _userService.GetUserByAccessTokenAsync(accessToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(Users user)
        {
            await _localStorageService.SetItemAsync("accessToken", user.AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", user.RefreshToken);

            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("accessToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(Users user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.Login != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, user.Login),
                                    new Claim(ClaimTypes.Role, user.Role.RoleDesc),
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }
    }
}
