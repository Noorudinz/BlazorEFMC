using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BethanysPieShopHRM.Shared
{
    public class Company
    {
        [Key]
        public int OrgId { get; set; }
        public string Org_Code { get; set; }
        public string Org_Name { get; set; }
        public string Org_Address { get; set; }
        public string Org_Zip    { get; set; }
        public string Org_Phone  { get; set; }
        public string Org_Email { get; set; }
        public string Org_Website { get; set; }
        public string Org_Remarks { get; set; }
        public string Org_Logo { get; set; }
        public DateTime? updated_Date { get; set; }
       

    }
}
