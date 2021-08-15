using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public class CompanyDataService : ICompanyDataService
    {
        private readonly HttpClient _httpClient;

        public CompanyDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Company>> GetCompany()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Company>>
               (await _httpClient.GetStreamAsync($"api/company"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Company> GetCompanyDetails(int orgId)
        {
            return await JsonSerializer.DeserializeAsync<Company>
                (await _httpClient.GetStreamAsync($"api/company/{orgId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateCompany(Company company)
        {
            var companyJson =
               new StringContent(JsonSerializer.Serialize(company), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/company", companyJson);
        }
    }
}
