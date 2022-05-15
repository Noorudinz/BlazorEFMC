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
    public class PaymentRepository: IPayment
    {
        private readonly HttpClient _httpClient;
        public PaymentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public CommonResponse AddReceipt(Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public List<AccountSummary> GetLastSummaryDetail()
        {
            throw new NotImplementedException();
        }

        public PriceFactor GetPriceFactor()
        {
            throw new NotImplementedException();
        }

        public List<Receipt> GetReceiptByReceiptNo(string flatNo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Receipt>> GetReceiptList()
        {
            return await JsonSerializer.DeserializeAsync<List<Receipt>>
                (await _httpClient.GetStreamAsync($"api/Payments/GetReceiptList"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public List<AccountSummary> GetSummaryByFlatNo(string flatNo)
        {
            throw new NotImplementedException();
        }

        public CommonResponse UpdatePriceFactor(PriceFactor priceFactor)
        {
            throw new NotImplementedException();
        }

        List<Receipt> IPayment.GetReceiptList()
        {
            throw new NotImplementedException();
        }
    }
}
