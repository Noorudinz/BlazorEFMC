using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Repository
{
    public interface IBuilding
    {
        Task<IEnumerable<Building>> GetAllBuildings();
        Task<Building> GetBuilding(int Id);
        Task<Building> AddBuilding(Building building);
        Task DeleteBuilding(int id, string code);
    }
}
