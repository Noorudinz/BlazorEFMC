using BethanysPieShopHRM.Api.Data;
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
    public class BuildingsController : ControllerBase
    {
        private readonly IBuilding _buildingRepo;

        public BuildingsController(IBuilding buildingRepo)
        {
            _buildingRepo = buildingRepo;
        }


        [HttpGet]
        [Route("GetBuildings")]
        public async Task<IActionResult> GetBuildings()
        {           
            return Ok(_buildingRepo.GetBuildings());
        }

        [HttpPost]
        [Route("AddOrUpdateBuilding")]
        public async Task<IActionResult> AddOrUpdateBuilding(Building building)
        {
            return Ok(_buildingRepo.AddOrUpdateBuilding(building));          
        }

        [HttpDelete]
        [Route("DeleteBuilding/{id}/{code}")]
        public async Task<IActionResult> DeleteUser(int id, string code)
        {
            return Ok(_buildingRepo.DeleteBuilding(id, code));
        }

        [HttpGet]
        [Route("GetBuildingById/{Id}")]
        public async Task<IActionResult> GetBuildingById(int Id)
        {
            return Ok(_buildingRepo.GetBuildingById(Id));
        }
    }
}
