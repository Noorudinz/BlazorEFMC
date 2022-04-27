using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatOwnerController : ControllerBase
    {
        private readonly IFlatOwner _flatOwnerRepo;

        public FlatOwnerController(IFlatOwner flatOwnerRepo)
        {
            _flatOwnerRepo = flatOwnerRepo;
        }

        [HttpGet]
        [Route("GetFlatOwners")]
        public async Task<IActionResult> GetFlatOwners()
        {
            return Ok(_flatOwnerRepo.GetFlatOwners());
        }

        [HttpGet]
        [Route("GetFlatOwnerByFlatNo/{flatNo}")]
        public async Task<IActionResult> GetFlatOwnerByFlatNo(string flatNo)
        {
            return Ok(_flatOwnerRepo.GetFlatOwnerByFlatNo(flatNo));
        }

        [HttpPost]
        [Route("AddFlatOwner")]
        public async Task<IActionResult> AddFlatOwner(FlatOwner flatOwner)
        {
            return Ok(_flatOwnerRepo.AddFlatOwner(flatOwner));
        }

        [HttpDelete]
        [Route("DeleteFlat/{flatNo}")]
        public async Task<IActionResult> DeleteFlat(string flatNo)
        {
            return Ok(_flatOwnerRepo.DeleteFlat(flatNo));
        }
    }
}
