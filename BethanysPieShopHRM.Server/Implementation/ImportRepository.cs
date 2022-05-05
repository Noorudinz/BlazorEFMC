using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Implementation
{
    public class ImportRepository: IImports
    {
        private readonly HttpClient _httpClient;
        public ImportRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BTU>> GetAllBTU()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<BTU>>
                (await _httpClient.GetStreamAsync($"api/Imports/GetBTUList"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Electricity>> GetAllElectricity()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Electricity>>
                (await _httpClient.GetStreamAsync($"api/Imports/GetElectricityList"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Water>> GetAllWater()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Water>>
               (await _httpClient.GetStreamAsync($"api/Imports/GetWaterList"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UploadBTU(List<FileData> fileData)
        {
            if (fileData.Count > 0)
            {
                var payload = new SaveFile { Files = fileData };
                await _httpClient.PostAsJsonAsync("api/Imports/SaveBTU", payload);
            }
        }

        public Task UploadElectricity()
        {
            throw new NotImplementedException();
        }

        public Task UploadWater()
        {
            throw new NotImplementedException();
        }
    }
}
