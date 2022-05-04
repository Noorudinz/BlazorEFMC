using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Implementation
{
    public class PriceFactorRepository: IPriceFactor
    {
        private readonly HttpClient _httpClient;
        public PriceFactorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PriceFactor> GetPriceFactor()
        {
            return await JsonSerializer.DeserializeAsync<PriceFactor>
                (await _httpClient.GetStreamAsync($"api/Payments/GetPriceFactor"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<PriceFactor> UpdatePriceFactor(PriceFactor priceFactor)
        {
            var priceFactorJson =
                new StringContent(JsonSerializer.Serialize(priceFactor), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Payments/UpdatePriceFactor", priceFactorJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<PriceFactor>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
    }
}
