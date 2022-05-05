using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class ImportsRepository: IImports
    {
        private readonly AppDbContext _context;

        public ImportsRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<BTU> GetBTUList()
        {
            var btuList = _context.BTU.OrderByDescending(x => x.ReadingDate).ToList();
            return (btuList);
        }

        public List<Electricity> GetElectricityList()
        {
            var electricityList = _context.Electricity.OrderByDescending(x => x.ReadingDate).ToList();
            return (electricityList);
        }

        public List<Water> GetWaterList()
        {
            var waterList = _context.Water.OrderByDescending(x => x.ReadingDate).ToList();
            return (waterList);
        }

        public CommonResponse UploadBTU(IFormFile file, string pathToSave, string folderName)
        {
            try
            {      
                if (file.Length > 0)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MMM-dd__HH-mm-ss__");
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullFileName = datetime + fileName;
                    var fullPath = Path.Combine(pathToSave, fullFileName);
                    var dbPath = Path.Combine(folderName, fullFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var btuList = ExcelUtility.ImportExcelUtility.Read(fullPath);

                    foreach (var btu in btuList)
                    {
                        var importBtu = new BTU();
                        importBtu.FlatNo = btu.FlatNo;
                        importBtu.MeterID = btu.MeterID;
                        importBtu.Reading = btu.Reading;
                        importBtu.ReadingDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importBtu.CreatedDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importBtu.flag = false;

                        _context.BTU.Add(importBtu);
                        _context.SaveChanges();
                    }

                    return (new CommonResponse()
                    {
                        Message = "Successfully BTU readings imported :" + btuList.Count().ToString() + " Counts",
                        IsUpdated = true
                    });
                }

                return (new CommonResponse()
                {
                    Message = "Bad request",
                    IsUpdated = false
                });

            }
            catch (Exception ex)
            {
                return (new CommonResponse()
                {
                    Message = ex.ToString(),
                    IsUpdated = false
                });
            }
        }

        public CommonResponse UploadElectricity(IFormFile file, string pathToSave, string folderName)
        {
            try
            {
                if (file.Length > 0)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MMM-dd__HH-mm-ss__");
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullFileName = datetime + fileName;
                    var fullPath = Path.Combine(pathToSave, fullFileName);
                    var dbPath = Path.Combine(folderName, fullFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var electricityList = ExcelUtility.ImportExcelUtility.Read(fullPath);

                    foreach (var electricity in electricityList)
                    {
                        var importElectricity = new Electricity();
                        importElectricity.FlatNo = electricity.FlatNo;
                        importElectricity.MeterID = electricity.MeterID;
                        importElectricity.Reading = electricity.Reading;
                        importElectricity.ReadingDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importElectricity.CreatedDate = new DateTime(2021, 12, 31);
                        importElectricity.flag = false;

                        _context.Electricity.Add(importElectricity);
                        _context.SaveChanges();
                    }

                    return (new CommonResponse()
                    {
                        Message = "Successfully Electricity readings imported :" + electricityList.Count().ToString() + " Counts",
                        IsUpdated = true
                    });
                }
                else
                {
                    return (new CommonResponse()
                    {
                        Message = "Bad request",
                        IsUpdated = false
                    });
                }
            }
            catch(Exception ex)
            {
                return (new CommonResponse()
                {
                    Message = ex.ToString(),
                    IsUpdated = false
                });
            }
        }

        public CommonResponse UploadWater(IFormFile file, string pathToSave, string folderName)
        {
            try
            {
                if (file.Length > 0)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MMM-dd__HH-mm-ss__");
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullFileName = datetime + fileName;
                    var fullPath = Path.Combine(pathToSave, fullFileName);
                    var dbPath = Path.Combine(folderName, fullFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var waterList = ExcelUtility.ImportExcelUtility.Read(fullPath);

                    foreach (var water in waterList)
                    {
                        var importWater = new Water();
                        importWater.FlatNo = water.FlatNo;
                        importWater.MeterID = water.MeterID;
                        importWater.Reading = water.Reading;
                        importWater.ReadingDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importWater.CreatedDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importWater.flag = false;

                        _context.Water.Add(importWater);
                        _context.SaveChanges();
                    }

                    return (new CommonResponse()
                    {
                        Message = "Successfully Water readings imported :" + waterList.Count().ToString() + " Counts",
                        IsUpdated = true
                    });
                }
                else
                {
                    return (new CommonResponse()
                    {
                        Message = "Bad request",
                        IsUpdated = false
                    });
                }
            }
            catch (Exception ex)
            {
                return (new CommonResponse()
                {
                    Message = ex.ToString(),
                    IsUpdated = false
                });
            }
        }

        public CommonResponse UploadBlazorBTU(SaveFile saveFile, string path, string folder)
        {
            try
            {
                if (saveFile != null)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MMM-dd__HH-mm-ss__");
                    string fileExtenstion = saveFile.Files[0].FileType.ToLower().Contains("xls") ? "xls" : "xlsx";
                    string fileName = $"{path}/{datetime}BTU.{fileExtenstion}";
                    using (var fileStream = System.IO.File.Create(fileName))
                    {
                        fileStream.WriteAsync(saveFile.Files[0].Data);
                    }

                    var btuList = ExcelUtility.ImportExcelUtility.Read(fileName);

                    foreach (var btu in btuList)
                    {
                        var importBtu = new BTU();
                        importBtu.FlatNo = btu.FlatNo;
                        importBtu.MeterID = btu.MeterID;
                        importBtu.Reading = btu.Reading;
                        importBtu.ReadingDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importBtu.CreatedDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importBtu.flag = false;

                        _context.BTU.Add(importBtu);
                        //_context.SaveChanges();
                    }

                    return (new CommonResponse()
                    {
                        Message = "Successfully BTU readings imported :" + btuList.Count().ToString() + " Counts",
                        IsUpdated = true
                    });
                }

                return (new CommonResponse()
                {
                    Message = "Bad request",
                    IsUpdated = false
                });

            }
            catch (Exception ex)
            {
                return (new CommonResponse()
                {
                    Message = ex.ToString(),
                    IsUpdated = false
                });
            }
        }

        public CommonResponse UploadBlazorElectricity(SaveFile saveFile, string path, string folder)
        {
            try
            {
                if (saveFile != null)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MMM-dd__HH-mm-ss__");
                    string fileExtenstion = saveFile.Files[0].FileType.ToLower().Contains("xls") ? "xls" : "xlsx";
                    string fileName = $"{path}/{datetime}Electricity.{fileExtenstion}";
                    using (var fileStream = System.IO.File.Create(fileName))
                    {
                        fileStream.WriteAsync(saveFile.Files[0].Data);
                    }

                    var electricityList = ExcelUtility.ImportExcelUtility.Read(fileName);

                    foreach (var electricity in electricityList)
                    {
                        var importElectricity = new Electricity();
                        importElectricity.FlatNo = electricity.FlatNo;
                        importElectricity.MeterID = electricity.MeterID;
                        importElectricity.Reading = electricity.Reading;
                        importElectricity.ReadingDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importElectricity.CreatedDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importElectricity.flag = false;

                        _context.Electricity.Add(importElectricity);
                        //_context.SaveChanges();
                    }

                    return (new CommonResponse()
                    {
                        Message = "Successfully Electricity readings imported :" + electricityList.Count().ToString() + " Counts",
                        IsUpdated = true
                    });
                }

                return (new CommonResponse()
                {
                    Message = "Bad request",
                    IsUpdated = false
                });

            }
            catch (Exception ex)
            {
                return (new CommonResponse()
                {
                    Message = ex.ToString(),
                    IsUpdated = false
                });
            }
        }

        public CommonResponse UploadBlazorWater(SaveFile saveFile, string path, string folder)
        {
            try
            {
                if (saveFile != null)
                {
                    string datetime = DateTime.Now.ToString("yyyy-MMM-dd__HH-mm-ss__");
                    string fileExtenstion = saveFile.Files[0].FileType.ToLower().Contains("xls") ? "xls" : "xlsx";
                    string fileName = $"{path}/{datetime}Water.{fileExtenstion}";
                    using (var fileStream = System.IO.File.Create(fileName))
                    {
                        fileStream.WriteAsync(saveFile.Files[0].Data);
                    }

                    var waterList = ExcelUtility.ImportExcelUtility.Read(fileName);

                    foreach (var water in waterList)
                    {
                        var importWater = new Water();
                        importWater.FlatNo = water.FlatNo;
                        importWater.MeterID = water.MeterID;
                        importWater.Reading = water.Reading;
                        importWater.ReadingDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importWater.CreatedDate = new DateTime(2021, 12, 31); //DateTime.Now;
                        importWater.flag = false;

                        _context.Water.Add(importWater);
                        //_context.SaveChanges();
                    }

                    return (new CommonResponse()
                    {
                        Message = "Successfully Water readings imported :" + waterList.Count().ToString() + " Counts",
                        IsUpdated = true
                    });
                }

                return (new CommonResponse()
                {
                    Message = "Bad request",
                    IsUpdated = false
                });

            }
            catch (Exception ex)
            {
                return (new CommonResponse()
                {
                    Message = ex.ToString(),
                    IsUpdated = false
                });
            }
        }

      
    }
}
