using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Repository
{
    public interface IEmail
    {
        Email GetEmail();
        CommonResponse UpdateEmailSetting(Email email);
        List<Email> GetEmailById(int Id);
    }
}
