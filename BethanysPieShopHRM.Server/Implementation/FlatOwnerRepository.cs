using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Implementation
{
    public class FlatOwnerRepository: IFlatOwner
    {
        private readonly HttpClient _httpClient;

        public FlatOwnerRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<FlatOwner> AddFlatOwner(FlatOwner flatOwner)
        {
            var flatOwnerJson =
                new StringContent(JsonSerializer.Serialize(flatOwner), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/FlatOwner/AddFlatOwner", flatOwnerJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<FlatOwner>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteFlatOwner(int flatId)
        {
            await _httpClient.DeleteAsync($"api/FlatOwner/DeleteFlat/{flatId}");
        }

        public async Task<IEnumerable<FlatOwner>> GetAllFlatOwners()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<FlatOwner>>
                (await _httpClient.GetStreamAsync($"api/FlatOwner/GetFlatOwners"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task<FlatOwner> GetFlatOwner(int flatId)
        {
            return await JsonSerializer.DeserializeAsync<FlatOwner>
                (await _httpClient.GetStreamAsync($"api/FlatOwner/GetFlatOwnerByFlatNo/{flatId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateFlatOwner(FlatOwner flatOwner)
        {
            var flatOwnerJson =
                 new StringContent(JsonSerializer.Serialize(flatOwner), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/FlatOwner/AddFlatOwner", flatOwnerJson);
        }
    }
}
