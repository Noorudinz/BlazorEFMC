using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
   
    public class DashboardRepo : IDashboard
    {
        private readonly AppDbContext _appDbContext;

        public DashboardRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Dashboard GetDashboardDetails()
        {
            var dashBoradDetail = new Dashboard();
            dashBoradDetail.FlatOwnersCount = _appDbContext.FlatOwner.Count();
            dashBoradDetail.TotalOutstandingPayments = Convert.ToDecimal(_appDbContext.Payment.Select(s => s.Arrears).Sum());

            var topFiveOP = _appDbContext.FlatOwner
                                        .Join(_appDbContext.Payment,
                                        FO => FO.FlatNo,
                                        OP => OP.FlatNo,
                                        (FO, OP) => new { FlatOwner = FO, OutPay = OP })
                                        .Select(t => new
                                        {
                                            Name = t.FlatOwner.FamilyName + ' ' + t.FlatOwner.FirstName,
                                            FlatNo = t.FlatOwner.FlatNo,
                                            Amount = t.OutPay.Arrears
                                        })
                                        .OrderByDescending(e => e.Amount)
                                        .Take(5).ToList();

            var listTop5 = new List<TopFiveOP>();

            foreach(var five in topFiveOP)
            {
                TopFiveOP tp5 = new TopFiveOP();
                tp5.Amount = Convert.ToDecimal(five.Amount);
                tp5.FlatNo = five.FlatNo;
                tp5.Name = five.Name;
                listTop5.Add(tp5);
            }

            dashBoradDetail.TopFiveOP = listTop5;

            var amountDue = _appDbContext.TotalAmountDue
                                    .FromSqlRaw("select sum(Amount) as Amount from AccountSummary a, FlatOwner f where a.AccontNo in (SELECT MAX(AccontNo)" +
                                                "FROM AccountSummary GROUP BY FlatNo) and(f.FlatNo = a.FlatNo)").ToList();

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            var lastMonthTotalBills = _appDbContext.Bills
                .Where(a => a.cycle_from.Date == first && a.cycle_to.Date == last)
                 .Sum(e => e.Amount);

            var lastMonthTotalRevenue = _appDbContext.Receipt
                 .Where(a => (a.CreatedDate.Value.Date >= first.Date) && (a.CreatedDate.Value.Date <= last.Date))
                 .Sum(e => e.AmountReceived);

            return dashBoradDetail;

        }
    }
}
