using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class BillGenerationRepository: IBillGeneration
    {
        private readonly AppDbContext _context;

        public BillGenerationRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Bills> GenerateBill(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public List<Bills> SendMailGeneratedBills(DateTime selectedDate)
        {
            var fromPeriod = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            var toPeriod = new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month));

            var recordsBeforeMailSend = _context.Bills
                .Where(f => f.cycle_from.Date >= fromPeriod
                && f.cycle_to.Date <= toPeriod && f.IsMailSend == false)
                .ToList();

            var emailSettings = _context.Email.FirstOrDefault();

            foreach (var bill in recordsBeforeMailSend)
            {
                ManageMails.MailSender.SendEmails(bill, emailSettings);

                var foundData = _context.Bills.FirstOrDefault(a => a.BillNo == bill.BillNo);

                foundData.IsMailSend = true;

                _context.SaveChanges();

            }

            ManageMails.MailSender.DeleteFiles();

            var recordsAfterMailSend = _context.Bills
               .Where(f => f.cycle_from.Date >= fromPeriod
               && f.cycle_to.Date <= toPeriod && f.IsMailSend == false)
               .ToList();

            return (recordsAfterMailSend);
        }
    }
}
