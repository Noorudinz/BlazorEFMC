using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Repository
{
    public interface IFlatOwner
    {
        List<FlatOwner> GetFlatOwners();
        FlatOwner GetFlatOwnerByFlatNo(string flatNo);
        FlatOwner GetFlatOwnerByFlatId(int flatId);
        CommonResponse AddFlatOwner(FlatOwner flatOwner);
        DeleteResponse DeleteFlat(string flatNo);

    }
}
