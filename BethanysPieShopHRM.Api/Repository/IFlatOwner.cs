using BethanysPieShopHRM.Api.Models;
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
        CommonResponse AddFlatOwner(FlatOwner flatOwner);
        DeleteResponse DeleteFlat(string flatNo);

    }
}
