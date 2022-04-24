using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Models
{
    public class UserList
    {
    }

    public class SendEmailResponse
    {
        public string Message { get; set; }
        public bool IsSend { get; set; }
    }

    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordResponse
    {
        public string Message { get; set; }
        public bool IsChanges { get; set; }
    }

    public class DeleteResponse
    {
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}
