using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Models
{
    public class CompanyReposistory : ICompanyReposistory
    {
        private readonly AppDbContext _appDbContext;

        public CompanyReposistory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Company> GetCompany()
        {
            return _appDbContext.Company;
        }

        public Company GetCompanyById(int org_Id)
        {
            return _appDbContext.Company.FirstOrDefault(c => c.OrgId == org_Id);
        }

        public Company UpdateCompany(Company company)
        {
            var foundCompany = _appDbContext.Company.FirstOrDefault(e => e.OrgId == company.OrgId);

            if (foundCompany != null)
            {   

                foundCompany.OrgId = company.OrgId;
                foundCompany.Org_Name = company.Org_Name;
                foundCompany.Org_Code = company.Org_Code;
                foundCompany.Org_Address = company.Org_Address;
                foundCompany.Org_Email = company.Org_Email;
                foundCompany.Org_Phone = company.Org_Phone;
                foundCompany.Org_Remarks = company.Org_Remarks;
                foundCompany.Org_Website = company.Org_Website;
                foundCompany.Org_Zip = company.Org_Zip;
                foundCompany.Org_Logo = company.Org_Logo;

                _appDbContext.SaveChanges();

                return foundCompany;
            }

            return null;
        }
    }
}
