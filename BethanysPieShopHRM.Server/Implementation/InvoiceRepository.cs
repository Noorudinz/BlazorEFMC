﻿using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Implementation
{
    public class InvoiceRepository: IInvoice
    {
        private readonly HttpClient _httpClient;
        public InvoiceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Bills>> GetMailListToSendByPeriods(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public async Task<Bills> InvoiceDetails(long billNo)
        {
            return await JsonSerializer.DeserializeAsync<Bills>
                (await _httpClient.GetStreamAsync($"api/Invoice/InvoiceDetail/{billNo}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public Task<List<Bills>> InvoiceByFlatNo(string flatNo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bills>> InvoiceByFlatNo()
        {
            return await JsonSerializer.DeserializeAsync<List<Bills>>
               (await _httpClient.GetStreamAsync($"api/Invoice/InvoiceByFlatNo"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Bills>> InvoiceByPeriods(string selectedDate)
        {
          return await JsonSerializer.DeserializeAsync<List<Bills>>
                (await _httpClient.GetStreamAsync($"api/Invoice/InvoiceByPeriods/{selectedDate}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
           
        }

        public Task<List<Bills>> InvoiceByBillNo(long billNo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Bills>> GenerateBills(string selectedDate)
        {
            return await JsonSerializer.DeserializeAsync<List<Bills>>
                 (await _httpClient.GetStreamAsync($"api/Invoice/GenerateBill/{selectedDate}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Bills>> SendBills(string selectedDate)
        {
            return await JsonSerializer.DeserializeAsync<List<Bills>>
                 (await _httpClient.GetStreamAsync($"api/Invoice/SendMailGeneratedBills/{selectedDate}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}