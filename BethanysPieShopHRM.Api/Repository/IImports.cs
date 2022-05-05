using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Repository
{
    public interface IImports
    {
        List<BTU> GetBTUList();
        List<Water> GetWaterList();
        List<Electricity> GetElectricityList();
        CommonResponse UploadBTU(IFormFile file, string path, string folder);
        CommonResponse UploadElectricity(IFormFile file, string path, string folder);
        CommonResponse UploadWater(IFormFile file, string path, string folder);
        CommonResponse UploadBlazorBTU(SaveFile saveFile, string path, string folder);
        CommonResponse UploadBlazorElectricity(SaveFile file, string path, string folder);
        CommonResponse UploadBlazorWater(SaveFile file, string path, string folder);

    }
}
