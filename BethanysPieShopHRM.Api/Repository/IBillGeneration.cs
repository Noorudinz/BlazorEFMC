using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Repository
{
    public interface IBillGeneration
    {
        List<Bills> SendMailGeneratedBills(DateTime selectedDate);
        List<Bills> GenerateBill(DateTime selectedDate);
    }
}
