﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Shared
{
    public class AccountSummary
    {
        [Key]
        public long AccontNo { get; set; }
        public string FlatNo { get; set; }        
        public string Narration { get; set; }
        public byte? Transtype { get; set; }
        public decimal? Charge { get; set; }
        public decimal? Receipts { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? created_date { get; set; }
        public long charge_id { get; set; }
        public long receipt_id { get; set; }
    }


    public class TotalAmountDue
    {
        [Key]
        public decimal? Amount { get; set; }
    }
}
