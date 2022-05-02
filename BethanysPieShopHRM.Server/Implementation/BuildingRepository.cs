using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;


namespace BethanysPieShopHRM.Server.Implementation
{
    
    public class BuildingRepository: IBuilding
    {
        private readonly HttpClient _httpClient;
        public BuildingRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Building>> GetAllBuildings()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Building>>
                (await _httpClient.GetStreamAsync($"api/Buildings/GetBuildings"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Building> AddBuilding(Building building)
        {
            var buildingJson =
                new StringContent(JsonSerializer.Serialize(building), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Buildings/AddOrUpdateBuilding", buildingJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Building>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteBuilding(int id, string code)
        {
            await _httpClient.DeleteAsync($"api/Buildings/DeleteBuilding/{id}/{code}");
        }


        public async Task<Building> GetBuilding(int Id)
        {
            return await JsonSerializer.DeserializeAsync<Building>
                (await _httpClient.GetStreamAsync($"api/Buildings/GetBuildingById/{Id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
