﻿using BethanysPieShopHRM.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;


namespace BethanysPieShopHRM.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
  
   
        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var registerAsJson = JsonSerializer.Serialize(registerModel);

            var response = await _httpClient.PostAsync("api/accounts", new StringContent(registerAsJson, Encoding.UTF8, "application/json"));
           
            var registerResult = JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!registerResult.Successful)
            {
                return registerResult;
                
            }

            return registerResult;

        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UserModel>>
                (await _httpClient.GetStreamAsync($"api/accounts/GetAllUsers"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task<UserRoleVM> GetUsersById(string userId)
        {
            return await JsonSerializer.DeserializeAsync<UserRoleVM>
                (await _httpClient.GetStreamAsync($"api/accounts/GetUserRolesById/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //return data.FirstOrDefault(a => a.Id == Id);

        }

        public async Task<RegisterResult> UpdateUserRoles(UserRoleVM userRolesModel)
        {
            var userRolesAsJson = JsonSerializer.Serialize(userRolesModel);

            var response = await _httpClient.PostAsync("api/accounts/RegisterUserRoles", new StringContent(userRolesAsJson, Encoding.UTF8, "application/json"));

            var registerResult = JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!registerResult.Successful)
            {
                return registerResult;

            }

            return registerResult;
        }
    }
}
