using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;

namespace BethanysPieShopHRM.Server.Repository
{
    public interface IFlatOwner
    {
        Task<IEnumerable<FlatOwner>> GetAllFlatOwners();
        Task<FlatOwner> GetFlatOwner(int flatId);
        Task<FlatOwner> AddFlatOwner(FlatOwner flatOwner);
        Task UpdateFlatOwner(FlatOwner flatOwner);
        Task DeleteFlatOwner(int flatId);
    }
}
