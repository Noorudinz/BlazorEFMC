using System;
using System.Collections.Generic;
using System.Text;

namespace BethanysPieShopHRM.Shared
{
    public class Dashboard
    {
        public int FlatOwnersCount { get; set; }
        public decimal TotalOutstandingPayments { get; set; }
        public decimal LastMonthRevenue { get; set; }
        public decimal CurrentMonthRevenue { get; set; }
        public List<TopFiveOP> TopFiveOP { get; set; }
    }

    public class TopFiveOP
    {
        public string FlatNo { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }


    //public class BarChartData
    //{
    //    public decimal BTU { get; set; }
    //    public decimal WATER { get; set; }
    //    public decimal ELECTRICITY { get; set; }
    //    public string Month { get; set; }
    //    public int Year { get; set; }
    //}


    //public class PieChartData
    //{
    //    public decimal Y { get; set; }
    //    public string Name { get; set; }
    //}

    //public class BarChart
    //{
    //    public string Type { get; set; } = "undefined";
    //    public string Name { get; set; }
    //    public decimal[] Data { get; set; }
    //}
}
