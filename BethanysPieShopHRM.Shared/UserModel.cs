using System;
using System.Collections.Generic;
using System.Text;

namespace BethanysPieShopHRM.Shared
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
