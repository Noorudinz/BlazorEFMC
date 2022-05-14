using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Repository
{
    public interface IInvoice
    {
        List<Bills> InvoiceByFlatNo(string flatNo);
        List<Bills> InvoiceByFlatNo();
        List<Bills> InvoiceByBillNo(Int64 billNo);
        List<Bills> InvoiceByPeriods(DateTime selectedDate);
        List<Bills> GetMailListToSendByPeriods(DateTime selectedDate);
        Bills InvoiceDetails(Int64 billNo);
        List<Bills> GenerateBill(DateTime selectedDate);
        List<Bills> SendMailBill(DateTime selectedDate);
    }
}
