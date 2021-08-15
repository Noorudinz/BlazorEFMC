using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public interface ICompanyDataService
    {
        Task<IEnumerable<Company>> GetCompany();
        Task<Company> GetCompanyDetails(int orgId);
        Task UpdateCompany(Company company);     
    }
}
