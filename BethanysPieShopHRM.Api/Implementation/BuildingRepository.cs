using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class BuildingRepository: IBuilding
    {
        private readonly AppDbContext _context;
        public BuildingRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public BuildingResponse AddOrUpdateBuilding(Building building)
        {
            if (building != null)
            {

                if (building.BuildingId != 0)
                {
                    var updateBuilding = _context.Building
                        .FirstOrDefault(e => e.BuildingId == building.BuildingId);

                    if (updateBuilding != null)
                    {
                        updateBuilding.BuildingName = building.BuildingName;
                        updateBuilding.BuildingCode = building.BuildingCode;
                        updateBuilding.BuildingIncharge = building.BuildingIncharge;
                        updateBuilding.Floors = building.Floors;
                        updateBuilding.ERF = building.ERF;
                        updateBuilding.ARF = building.ARF;
                        updateBuilding.WRF = building.WRF;
                        updateBuilding.Remarks = building.Remarks;
                        updateBuilding.TimeStamp = DateTime.Now;
                        updateBuilding.updated_ByUserId = building.updated_ByUserId;
                        updateBuilding.BuildingId = building.BuildingId;

                        _context.SaveChanges();

                        return (new BuildingResponse()
                        {
                            Message = "Building Updated Successfully !",
                            IsUpdated = true
                        });
                    }
                }
                else
                {
                    var addBuilding = new Building();

                    addBuilding.BuildingName = building.BuildingName;
                    addBuilding.BuildingCode = building.BuildingCode;
                    addBuilding.BuildingIncharge = building.BuildingIncharge;
                    addBuilding.Floors = building.Floors;
                    addBuilding.ERF = building.ERF;
                    addBuilding.ARF = building.ARF;
                    addBuilding.WRF = building.WRF;
                    addBuilding.Remarks = building.Remarks;
                    addBuilding.TimeStamp = DateTime.Now;
                    addBuilding.created_ByUserId = building.created_ByUserId;

                    _context.Building.Add(addBuilding);
                    _context.SaveChanges();

                    return (new BuildingResponse()
                    {
                        Message = "Building Added Successfully !",
                        IsUpdated = true
                    });

                }
            }

            return (new BuildingResponse()
            {
                Message = "Invalid request !",
                IsUpdated = false
            });
        }

        public DeleteResponse DeleteBuilding(int id, string code)
        {
            var foundBuilding = _context.Building.FirstOrDefault(w => w.BuildingId == id && w.BuildingCode == code);

            if (foundBuilding != null)
            {
                _context.Building.Remove(foundBuilding);
                _context.SaveChanges();

                return (new DeleteResponse()
                {
                    Message = "Deleted Successfully",
                    IsDeleted = true
                });

            }

            return (new DeleteResponse()
            {
                Message = "Something went wrong on delete building   !",
                IsDeleted = false
            });
        }

        public Building GetBuildingById(int Id)
        {
            var buildings = _context.Building.Where(f => f.BuildingId == Id).FirstOrDefault();
            return (buildings);
        }

        public List<Building> GetBuildings()
        {
            var buildings = _context.Building.ToList();
            return (buildings);
        }
    }
}
