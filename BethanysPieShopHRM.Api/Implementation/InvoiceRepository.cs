using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class InvoiceRepository: IInvoice
    {
        private readonly AppDbContext _context;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Bills> GetMailListToSendByPeriods(DateTime selectedDate)
        {
            var fromPeriod = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            var toPeriod = new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month));

            var records = _context.Bills
                .Where(f => f.cycle_from.Date >= fromPeriod
                && f.cycle_to.Date <= toPeriod && f.IsMailSend == false)
                .ToList();

            return (records);
        }

        public List<Bills> InvoiceByBillNo(long billNo)
        {
            var records = _context.Bills.Where(f => f.BillNo == billNo).ToList();

            return (records);
        }

        public Bills InvoiceDetails(long billNo)
        {
            var records = _context.Bills.Where(f => f.BillNo == billNo).FirstOrDefault();

            return (records);
        }

        public List<Bills> InvoiceByFlatNo(string flatNo)
        {
            var flatDetail = _context.FlatOwner.FirstOrDefault(f => f.FlatNo == flatNo);

            var priceFactor = _context.PriceFactor.FirstOrDefault();

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var cycleFrom = month.AddMonths(-1);
            var cycleTo = month.AddDays(-1);

            //------------List-------------------------------
            var btuList = _context.BTU
                .OrderByDescending(a => a.ReadingDate)
                .Where(r => r.FlatNo == flatNo).ToList();

            var waterList = _context.Water
               .OrderByDescending(a => a.ReadingDate)
               .Where(r => r.FlatNo == flatNo).ToList();

            var electricityList = _context.Electricity
               .OrderByDescending(a => a.ReadingDate)
               .Where(r => r.FlatNo == flatNo).ToList();

            //------------Current reading---------------------------

            var btuCurrentReading = btuList
                .Where(a => Convert.ToDateTime(a.ReadingDate).Month == DateTime.Now.Month)
                .FirstOrDefault();

            var waterCurrentReading = waterList
               .Where(a => Convert.ToDateTime(a.ReadingDate).Month == DateTime.Now.Month)
               .FirstOrDefault();

            var electCurrentReading = electricityList
               .Where(a => Convert.ToDateTime(a.ReadingDate).Month == DateTime.Now.Month)
               .FirstOrDefault();

            //------------Previous reading------------------------------

            var btuPreviousReading = _context.Bills
                .OrderByDescending(o => o.cycle_to)
                .Where(f => f.FlatNo == flatNo).First();

            var waterPreviousReading = _context.Bills
               .OrderByDescending(o => o.cycle_to)
               .Where(f => f.FlatNo == flatNo).First();

            var electPreviousReading = _context.Bills
               .OrderByDescending(o => o.cycle_to)
               .Where(f => f.FlatNo == flatNo).First();

            //----------------------Consumption--------------------------------------

            var btuConsumption = btuCurrentReading.Reading - btuPreviousReading.btu_prev;
            var waterConsumption = waterCurrentReading.Reading - waterPreviousReading.water_prev;
            var electConsumption = electCurrentReading.Reading - electPreviousReading.elec_prev;

            //---------------------Charge-------------------------------------------

            var btuAmount = btuConsumption * priceFactor.BTUFactor;
            var waterAmount = waterConsumption * priceFactor.WaterFactor;
            var electAmount = electConsumption * priceFactor.ElectricityFactor;

            //----------------------Previous arrear-----------------------------------------------
            var lastBillDetails = _context.Bills
               .OrderBy(o => o.BillNo)
               .Where(f => f.FlatNo == flatNo).Last();

            var bill = new Bills();
            string buildingName = _context.Building.FirstOrDefault(f => f.BuildingId == flatDetail.BuildingId).BuildingName;

            bill.FlatNo = flatNo;
            bill.cycle_from = cycleFrom;
            bill.cycle_to = cycleTo;
            bill.BTU_amount = btuAmount;
            bill.electricity_amount = electAmount;
            bill.water_amount = waterAmount;
            bill.service_charge = priceFactor.ServiceCharge;
            bill.other_charge = priceFactor.OtherCharges;
            bill.current_bill = btuAmount + waterAmount + electAmount;
            bill.previous_arrear = lastBillDetails.previous_arrear;
            bill.Amount = bill.current_bill + bill.previous_arrear;
            bill.paid = 0;
            bill.created_date = DateTime.Now;
            bill.due_date = DateTime.Now.AddDays(10);
            bill.elec_current = electCurrentReading.Reading;
            bill.elec_prev = electPreviousReading.elec_prev;
            bill.elec_consum = electConsumption;
            bill.water_current = waterCurrentReading.Reading;
            bill.water_prev = waterPreviousReading.water_prev;
            bill.water_consum = waterConsumption;
            bill.btu_current = btuCurrentReading.Reading;
            bill.btu_prev = btuPreviousReading.btu_prev;
            bill.btu_consum = btuConsumption;
            bill.FirstName = flatDetail.FirstName;
            bill.FloorNo = flatDetail.FloorNo;
            bill.BuildingName = buildingName;
            bill.MobileNumber = flatDetail.MobileNumber;
            bill.Email1 = flatDetail.Email1;
            bill.Address = flatDetail.Address;
            bill.FaxNumber = null;

            _context.Bills.Add(bill);
            _context.SaveChanges();

            var records = _context.Bills.OrderByDescending(o => o.BillNo)
                .Where(f => f.FlatNo == flatNo).Take(100).ToList();

            return (records);
        }

        public List<Bills> InvoiceByFlatNo()
        {
            var records = _context.Bills.OrderByDescending(o => o.BillNo)
                          .Take(100).ToList();

            return (records);
        }

        public List<Bills> InvoiceByPeriods(DateTime selectedDate)
        {
            var fromPeriod = new DateTime(selectedDate.Year, selectedDate.Month, 1);
            var toPeriod = new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month));

            var records = _context.Bills
                .Where(f => f.cycle_from.Date >= fromPeriod
                && f.cycle_to.Date <= toPeriod)
                .ToList();

            return (records);
        }

        public List<Bills> GenerateBill(DateTime selectedDate)
        {
            //try
            //{

                var fromPeriod = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                var toPeriod = new DateTime(selectedDate.Year, selectedDate.Month, DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month));

                var billsDetails = _context.Bills
                    .Where(f => f.cycle_from.Date >= fromPeriod
                    && f.cycle_to.Date <= toPeriod).Count();


                if (billsDetails == 0)
                {
                    var btuList = _context.BTU.Where(f => f.ReadingDate.Value.Date >= fromPeriod
                    && f.ReadingDate.Value.Date <= toPeriod).ToList();

                    var waterList = _context.Water.Where(f => f.ReadingDate.Value.Date >= fromPeriod
                    && f.ReadingDate.Value.Date <= toPeriod).ToList();

                    var electricityList = _context.Electricity.Where(f => f.ReadingDate.Value.Date >= fromPeriod
                    && f.ReadingDate.Value.Date <= toPeriod).ToList();

                    var priceFactor = _context.PriceFactor.FirstOrDefault();

                    foreach (var flatOwner in _context.FlatOwner)
                    {
                        //---------------flat details----------------------------------------------
                        var flatDetail = _context.FlatOwner.FirstOrDefault(f => f.FlatNo == flatOwner.FlatNo);

                        //--------------get imported reading by flat no------------------------------
                        var getBTUbyFlatNo = btuList.FirstOrDefault(e => e.FlatNo == flatOwner.FlatNo);
                        var getWaterbyFlatNo = waterList.FirstOrDefault(e => e.FlatNo == flatOwner.FlatNo);
                        var getElectbyFlatNo = electricityList.FirstOrDefault(e => e.FlatNo == flatOwner.FlatNo);

                        //------------current readings-----------------------------------------
                        decimal? currentBTU = getBTUbyFlatNo.Reading;
                        decimal? currentWater = getWaterbyFlatNo.Reading;
                        decimal? currentElect = getElectbyFlatNo.Reading;

                        //-------------Previous readings---------------------------------------

                        var lastBill = _context.Bills.OrderByDescending(o => o.cycle_from)
                            .ThenByDescending(t => t.cycle_to)
                            .FirstOrDefault(e => e.FlatNo == flatOwner.FlatNo);


                        decimal? previousBTU = lastBill.btu_current;
                        decimal? previousWater = lastBill.water_current;
                        decimal? previousElect = lastBill.elec_current;

                        //----------------Consumption---------------------------------------

                        var btuConsumption = currentBTU - previousBTU;
                        var waterConsumption = currentWater - previousWater;
                        var electConsumption = currentElect - previousElect;

                        //----------------Charge-------------------------------------------

                        var btuAmount = btuConsumption * priceFactor.BTUFactor;
                        var waterAmount = waterConsumption * priceFactor.WaterFactor;
                        var electAmount = electConsumption * priceFactor.ElectricityFactor;


                        var bill = new Bills();
                        string buildingName = _context.Building.FirstOrDefault(f => f.BuildingId == flatDetail.BuildingId).BuildingName;

                        bill.FlatNo = flatDetail.FlatNo;
                        bill.cycle_from = fromPeriod;
                        bill.cycle_to = toPeriod;
                        bill.BTU_amount = btuAmount;
                        bill.electricity_amount = electAmount;
                        bill.water_amount = waterAmount;
                        bill.service_charge = priceFactor.ServiceCharge;
                        bill.other_charge = priceFactor.OtherCharges;
                        bill.current_bill = btuAmount + waterAmount + electAmount;
                        bill.previous_arrear = lastBill.previous_arrear;
                        bill.Amount = bill.current_bill + bill.previous_arrear;
                        bill.paid = 0;
                        bill.created_date = toPeriod; //DateTime.Now;
                        bill.due_date = toPeriod.AddDays(10); //DateTime.Now.AddDays(10);
                        bill.elec_current = currentElect;
                        bill.elec_prev = previousElect;
                        bill.elec_consum = electConsumption;
                        bill.water_current = currentWater;
                        bill.water_prev = previousWater;
                        bill.water_consum = waterConsumption;
                        bill.btu_current = currentBTU;
                        bill.btu_prev = previousBTU;
                        bill.btu_consum = btuConsumption;
                        bill.FirstName = flatDetail.FirstName;
                        bill.FloorNo = flatDetail.FloorNo;
                        bill.BuildingName = buildingName;
                        bill.MobileNumber = flatDetail.MobileNumber;
                        bill.Email1 = flatDetail.Email1;
                        bill.Address = flatDetail.Address;
                        bill.FaxNumber = null;

                        _context.Bills.Add(bill);

                    }

                    _context.SaveChanges();

                    //--------------Accounts summary----------------------------------

                    foreach (var flatOwner in _context.FlatOwner)
                    {

                        var lastSummaryByFlatNo = _context.AccountSummary.Where(e => e.FlatNo == flatOwner.FlatNo)
                         .OrderByDescending(a => a.AccontNo)
                         .Take(1).FirstOrDefault();

                        var lastBillDetailsByFlatNo = _context.Bills.Where(e => e.FlatNo == flatOwner.FlatNo)
                            .OrderBy(e => e.BillNo).Last();

                        var accounts = new AccountSummary();
                        accounts.FlatNo = flatOwner.FlatNo;
                        accounts.Narration = "Utility bill month of" + toPeriod.ToString("MMM/yyyy");
                        accounts.Charge = lastBillDetailsByFlatNo.current_bill;
                        accounts.Receipts = Convert.ToDecimal(0.000);
                        accounts.Amount = lastSummaryByFlatNo.Amount + (lastBillDetailsByFlatNo.current_bill);
                        accounts.created_date = DateTime.Now;
                        accounts.charge_id = lastBillDetailsByFlatNo.BillNo;
                        accounts.receipt_id = 0;

                        _context.AccountSummary.Add(accounts);

                    }

                    _context.SaveChanges();

                    //-------------Payment Arrears-------------------------------------------------------
                    foreach (var flatOwner in _context.FlatOwner)
                    {
                        var lastBillDetailsByFlatNo = _context.Bills.Where(e => e.FlatNo == flatOwner.FlatNo)
                        .OrderBy(e => e.BillNo).Last();

                        var lastPay = _context.Payment.FirstOrDefault(e => e.FlatNo == flatOwner.FlatNo);
                        lastPay.Arrears = lastPay.Arrears + lastBillDetailsByFlatNo.current_bill;
                        lastPay.updated_date = DateTime.Now;
                    }

                    _context.SaveChanges();

                }

                var billRecords = _context.Bills
                                .Where(f => f.cycle_from.Date >= fromPeriod
                                 && f.cycle_to.Date <= toPeriod).ToList();

                return billRecords;
            //}
            //catch (Exception ex)
            //{
            //    return ex.ToString();
            //}
        }
    }
}
