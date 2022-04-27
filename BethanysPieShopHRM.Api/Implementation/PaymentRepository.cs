using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class PaymentRepository: IPayment
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public CommonResponse AddReceipt(Receipt receipt)
        {
            if (receipt != null)
            {
                var flatDet = _context.FlatOwner.FirstOrDefault(e => e.FlatNo == receipt.FlatNo);

                if (flatDet != null)
                {
                    var addReceipt = new Receipt();

                    addReceipt.FlatNo = flatDet.FlatNo;
                    addReceipt.PaymentMode = receipt.PaymentMode;
                    addReceipt.ChequeNo = receipt.ChequeNo;
                    addReceipt.ChequeDate = receipt.ChequeDate;
                    addReceipt.Bank = receipt.Bank;
                    addReceipt.AmountReceived = receipt.AmountReceived;
                    addReceipt.ReceivedBy = receipt.ReceivedBy;
                    addReceipt.Narration = receipt.Narration;
                    addReceipt.CreatedDate = DateTime.Now;

                    _context.Receipt.Add(addReceipt);
                    _context.SaveChanges();

                    var accounts = new AccountSummary();

                    var lastSummaryByFlatNo = _context.AccountSummary.Where(e => e.FlatNo == flatDet.FlatNo)
                        .OrderByDescending(a => a.AccontNo)
                        .Take(1).FirstOrDefault();

                    var lastReceiptDetailsByFlatNo = _context.Receipt.Where(e => e.FlatNo == flatDet.FlatNo)
                            .OrderBy(e => e.ReceiptNo).Last();

                    accounts.FlatNo = receipt.FlatNo;
                    accounts.Narration = receipt.Narration;
                    accounts.Charge = 0;
                    accounts.Receipts = receipt.AmountReceived;
                    accounts.Amount = lastSummaryByFlatNo.Amount - (receipt.AmountReceived);
                    accounts.created_date = DateTime.Now;
                    accounts.charge_id = 0;
                    accounts.receipt_id = lastReceiptDetailsByFlatNo.ReceiptNo;

                    _context.AccountSummary.Add(accounts);
                    _context.SaveChanges();

                    var lastPay = _context.Payment.FirstOrDefault(e => e.FlatNo == flatDet.FlatNo);
                    lastPay.Arrears = lastPay.Arrears - receipt.AmountReceived;
                    lastPay.updated_date = DateTime.Now;

                    _context.SaveChanges();

                    return (new CommonResponse()
                    {
                        Message = "Receipt created Successfully !",
                        IsUpdated = true
                    });
                }
            }

            return (new CommonResponse()
            {
                Message = "Invalid request !",
                IsUpdated = false
            });
        }

        public List<AccountSummary> GetLastSummaryDetail()
        {
            List<AccountSummary> summary = new List<AccountSummary>();

            foreach (var flatDet in _context.FlatOwner)
            {
                var filteredRecords = _context.AccountSummary.Where(e => e.FlatNo == flatDet.FlatNo)
                    .OrderBy(e => e.AccontNo).Last();

                summary.Add(filteredRecords);
            }

            var sortedRecords = summary.OrderByDescending(e => e.AccontNo).ToList();

            return (sortedRecords);
        }

        public List<Receipt> GetReceiptByReceiptNo(string flatNo)
        {
            var records = _context.Receipt.Where(f => f.FlatNo == flatNo)
               .OrderByDescending(e => e.ReceiptNo)
               .ToList();

            return (records);
        }

        public List<Receipt> GetReceiptList()
        {
            var receiptList = _context.Receipt.Take(1000).OrderByDescending(e => e.ReceiptNo)
             .ToList();

            return (receiptList);
        }

        public List<AccountSummary> GetSummaryByFlatNo(string flatNo)
        {
            var records = _context.AccountSummary.Where(f => f.FlatNo == flatNo)
               .OrderByDescending(e => e.AccontNo)
               .ToList();

            return (records);
        }
    }
}
