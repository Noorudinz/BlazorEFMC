using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyReposistory _companyRepository;

        public CompanyController(ICompanyReposistory companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]  
        public IActionResult GetCompany()
        {
            return Ok(_companyRepository.GetCompany());
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            return Ok(_companyRepository.GetCompanyById(id));
        }

        [HttpPut]
        public IActionResult UpdateCompany([FromBody] Company company)
        {
            if (company == null)
                return BadRequest();

            //if (company.Org_Name == string.Empty || company.Org_Address == string.Empty)
            //{
            //    ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyToUpdate = _companyRepository.GetCompanyById(company.OrgId);

            if (companyToUpdate == null)
                return NotFound();

            _companyRepository.UpdateCompany(company);

            return NoContent(); //success
        }
    }
}
