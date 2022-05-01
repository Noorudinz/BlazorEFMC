using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
    }
}
