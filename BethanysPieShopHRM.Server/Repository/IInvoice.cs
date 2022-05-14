using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Repository
{
    public interface IInvoice
    {
        Task<List<Bills>> InvoiceByFlatNo(string flatNo);
        Task<List<Bills>> InvoiceByFlatNo();
        Task<List<Bills>> InvoiceByBillNo(Int64 billNo);
        Task<List<Bills>> InvoiceByPeriods(string selectedDate);
        Task<List<Bills>> GetMailListToSendByPeriods(DateTime selectedDate);
        Task<Bills> InvoiceDetails(long billNo);
        Task<List<Bills>> GenerateBills(string selectedDate);
        Task<List<Bills>> SendBills(string selectedDate);

    }
}
