using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BethanysPieShopHRM.Server.Repository
{
    public interface IPayment
    {
        public Task<List<Receipt>> GetReceiptList();
        public Task<Receipt> GetReceiptByReceiptNo(string flatNo);
        public Task<List<AccountSummary>> GetLastSummaryDetail();
        public Task<List<AccountSummary>> GetSummaryByFlatNo(string flatNo);
        public Task<CommonResponse> AddReceipt(Receipt receipt);
        public Task<PriceFactor> GetPriceFactor();
        public Task<CommonResponse> UpdatePriceFactor(PriceFactor priceFactor);
    }
}
