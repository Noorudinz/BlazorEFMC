using System;
using System.Collections.Generic;
using System.Text;

namespace BethanysPieShopHRM.Shared
{
    public class RegistrationResponse: AuthResult
    {
        public string Message { get; set; }
        public bool IsCreated { get; set; }
    }
}
