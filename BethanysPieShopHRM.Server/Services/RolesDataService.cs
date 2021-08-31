using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public class RolesDataService : IRolesDataService
    {
        private readonly HttpClient _httpClient;

        public RolesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<RolesResultVM> AddRoleOrEdit(RolesVM role)
        {
            try
            {
                var roleAsJson = JsonSerializer.Serialize(role);

                var response = await _httpClient.PostAsync("api/roles", new StringContent(roleAsJson, Encoding.UTF8, "application/json"));

                var registerResult = JsonSerializer.Deserialize<RolesResultVM>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!registerResult.IsSuccess)
                {
                    return registerResult;
                }

                return registerResult;
            }
            catch(Exception ex)
            {
                return null;           
            }
          
        }

        public async Task<RolesResultVM> DeleteRole(RolesVM role)
        {
            try
            {
                var roleAsJson = JsonSerializer.Serialize(role);

                var response = await _httpClient.PostAsync("api/roles/DeleteRole", new StringContent(roleAsJson, Encoding.UTF8, "application/json"));

                var deleteResult = JsonSerializer.Deserialize<RolesResultVM>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!deleteResult.IsSuccess)
                {
                    return deleteResult;
                }

                return deleteResult;
            }
            catch (Exception ex)
            {
                return null;
            }
            // await _httpClient.DeleteAsync($"api/roles/{roleId}");
        }

        public async Task<IEnumerable<RolesVM>> GetAllRoles()
        {
            var rolesList = await JsonSerializer.DeserializeAsync<IEnumerable<RolesVM>>
                (await _httpClient.GetStreamAsync($"api/roles"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return rolesList;
        }

        public async Task<RolesVM> GetRoleDetails(string roleId)
        {
            return await JsonSerializer.DeserializeAsync<RolesVM>
              (await _httpClient.GetStreamAsync($"api/roles/{roleId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public Task UpdateRole(RolesVM role)
        {
            throw new NotImplementedException();
        }
    }
   
}
