using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Shared
{
    public class Building
    {
        public int BuildingId { get; set; }

        [Required]
        public string BuildingName { get; set; }

        [Required]
        public string BuildingCode { get; set; }
        public string BuildingIncharge { get; set; }
        public string Floors { get; set; }

        [Required]
        public decimal? ERF { get; set; }

        [Required]
        public decimal? ARF { get; set; }

        [Required]
        public decimal? WRF { get; set; }
        public string Remarks { get; set; }
        public string created_ByUserId { get; set; }
        public string updated_ByUserId { get; set; }
        public DateTime? TimeStamp { get; set; }
    }

    public class BuildingResponse
    {
        public string Message { get; set; }
        public bool IsUpdated { get; set; }
    }
}
