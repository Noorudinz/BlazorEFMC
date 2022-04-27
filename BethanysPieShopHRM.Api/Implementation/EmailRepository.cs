using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class EmailRepository: IEmail
    {
        private readonly AppDbContext _context;
        public EmailRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Email GetEmail()
        {
            var priceFactor = _context.Email.FirstOrDefault(id => id.id == 1);
            return (priceFactor);
        }

        public List<Email> GetEmailById(int Id)
        {
            var email = _context.Email.Where(f => f.id == Id).ToList();
            return (email);
        }

        public CommonResponse UpdateEmailSetting(Email email)
        {
            if (email != null)
            {
                var foundData = _context.Email.FirstOrDefault(a => a.id == 1);
                foundData.EmailAddress = email.EmailAddress;
                foundData.Password = email.Password;
                foundData.Host = email.Host;
                foundData.Port = email.Port;
                foundData.CC = email.CC;
                foundData.Updated_Date = DateTime.Now;

                _context.SaveChanges();

                return (new CommonResponse()
                {
                    Message = "SMTP Email settings updated successfully !",
                    IsUpdated = true
                });
            }

            return (new CommonResponse()
            {
                Message = "Invalid request!",
                IsUpdated = false
            });
        }
    }
}
