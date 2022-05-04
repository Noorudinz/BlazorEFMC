using BethanysPieShopHRM.Server.Repository;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Implementation
{
    public class EmailRepository: IEmailSetting
    {
        private readonly HttpClient _httpClient;
        public EmailRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Email> GetEmailSettingDetail()
        {
            return await JsonSerializer.DeserializeAsync<Email>
                (await _httpClient.GetStreamAsync($"api/Email/GetEmail"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Email> UpdateEmailSettingDetail(Email email)
        {
            var emailJson =
                new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Email/UpdateEmailSetting", emailJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Email>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
    }
}
