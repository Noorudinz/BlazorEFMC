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
    public class PaymentRepository: IPayment
    {
        private readonly HttpClient _httpClient;
        public PaymentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Receipt>> GetReceiptList()
        {
            return await JsonSerializer.DeserializeAsync<List<Receipt>>
                 (await _httpClient.GetStreamAsync($"api/Payments/GetReceiptList"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<CommonResponse> AddReceipt(Receipt receipt)
        {
            var receiptJson =
               new StringContent(JsonSerializer.Serialize(receipt), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Payments/AddReceipt", receiptJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<CommonResponse>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<List<AccountSummary>> GetLastSummaryDetail()
        {
            return await JsonSerializer.DeserializeAsync<List<AccountSummary>>
                (await _httpClient.GetStreamAsync($"api/Payments/GetLastSummaryDetail"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        Task<PriceFactor> IPayment.GetPriceFactor()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Receipt>> GetReceiptByReceiptNo(string flatNo)
        {
            return await JsonSerializer.DeserializeAsync<List<Receipt>>
                (await _httpClient.GetStreamAsync($"api/Payments/GetReceiptByReceiptNo/{flatNo}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<AccountSummary>> GetSummaryByFlatNo(string flatNo)
        {
            return await JsonSerializer.DeserializeAsync<List<AccountSummary>>
               (await _httpClient.GetStreamAsync($"api/Payments/GetSummaryByFlatNo/{flatNo}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        Task<CommonResponse> IPayment.UpdatePriceFactor(PriceFactor priceFactor)
        {
            throw new NotImplementedException();
        }
    }
}
