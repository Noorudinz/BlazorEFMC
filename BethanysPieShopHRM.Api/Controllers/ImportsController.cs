using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportsController : ControllerBase
    {
        private readonly IImports _importsRepo;
        public ImportsController(IImports importsRepo)
        {
            _importsRepo = importsRepo;
        }

        [HttpGet]
        [Route("GetBTUList")]
        public async Task<IActionResult> GetBTUList()
        {         
            return Ok(_importsRepo.GetBTUList());
        }

        [HttpGet]
        [Route("GetWaterList")]
        public async Task<IActionResult> GetWaterList()
        {         
            return Ok(_importsRepo.GetWaterList());
        }

        [HttpGet]
        [Route("GetElectricityList")]
        public async Task<IActionResult> GetElectricityList()
        {
            return Ok(_importsRepo.GetElectricityList());
        }

       

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadBTU")]
        public async Task<IActionResult> UploadBTU() //for API test use (IFormFile File) param
        {          
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Imports/BTU");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Ok(_importsRepo.UploadBTU(file, pathToSave, folderName));         
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadWater")]
        public async Task<IActionResult> UploadWater()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Imports/Water");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Ok(_importsRepo.UploadWater(file, pathToSave, folderName));          
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadElectricity")]
        public async Task<IActionResult> UploadElectricity()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Imports/Electricity");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Ok(_importsRepo.UploadElectricity(file, pathToSave, folderName));
          
        }

        [HttpPost("SaveBTU")]
        public async Task<IActionResult> SaveBTU([FromBody] SaveFile saveFile)
        {          
            var folderName = Path.Combine("Resources", "Imports/BTU");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Ok(_importsRepo.UploadBlazorBTU(saveFile, pathToSave, folderName));
        }


        [HttpPost("SaveElectricity")]
        public async Task<IActionResult> SaveElectricity([FromBody] SaveFile saveFile)
        {
            var folderName = Path.Combine("Resources", "Imports/Electricity");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Ok(_importsRepo.UploadBlazorElectricity(saveFile, pathToSave, folderName));
        }

        [HttpPost("SaveWater")]
        public async Task<IActionResult> SaveWater([FromBody] SaveFile saveFile)
        {
            var folderName = Path.Combine("Resources", "Imports/Water");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            return Ok(_importsRepo.UploadBlazorWater(saveFile, pathToSave, folderName));
        }
    }

}
