using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Repository
{
    public interface IImports
    {
        Task<IEnumerable<BTU>> GetAllBTU();
        Task<IEnumerable<Electricity>> GetAllElectricity();
        Task<IEnumerable<Water>> GetAllWater();
        Task UploadBTU(List<FileData> fileData);
        Task UploadElectricity(List<FileData> fileData);
        Task UploadWater(List<FileData> fileData);
    }
}
