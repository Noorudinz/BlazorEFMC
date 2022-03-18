﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Shared
{
    public class Water
    {
        [Key]
        public Int64 ID { get; set; }
        public string FlatNo { get; set; }
        public string MeterID { get; set; }
        public decimal? Reading { get; set; }
        public DateTime? ReadingDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? flag { get; set; }
    }
}
