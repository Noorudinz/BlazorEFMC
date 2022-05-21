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

        Task<List<AccountSummary>> IPayment.GetLastSummaryDetail()
        {
            throw new NotImplementedException();
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

        Task<List<AccountSummary>> IPayment.GetSummaryByFlatNo(string flatNo)
        {
            throw new NotImplementedException();
        }

        Task<CommonResponse> IPayment.UpdatePriceFactor(PriceFactor priceFactor)
        {
            throw new NotImplementedException();
        }
    }
}
