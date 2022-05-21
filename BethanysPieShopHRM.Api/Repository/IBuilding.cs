using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Repository
{
    public interface IBuilding
    {
        List<Building> GetBuildings();
        BuildingResponse AddOrUpdateBuilding(Building building);
        DeleteResponse DeleteBuilding(int id, string code);
        Building GetBuildingById(int Id);
    }
}
