using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Models
{
    public interface ICompanyReposistory
    {
        IEnumerable<Company> GetCompany();
        Company GetCompanyById(int org_Id);
        Company UpdateCompany(Company company);
    }

 
}
