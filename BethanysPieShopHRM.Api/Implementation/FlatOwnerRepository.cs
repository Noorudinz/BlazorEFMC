using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Api.Models;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class FlatOwnerRepository: IFlatOwner
    {
        private readonly AppDbContext _context;
        public FlatOwnerRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public CommonResponse AddFlatOwner(Shared.FlatOwner flatOwner)
        {
            return (new CommonResponse()
            {
                Message = "Flat Owner Added Successfully !",
                IsUpdated = true
            });
        }

        public DeleteResponse DeleteFlat(string flatNo)
        {
            return (new DeleteResponse()
            {
                Message = "Deleted Successfully",
                IsDeleted = true
            });
        }

        public Shared.FlatOwner GetFlatOwnerByFlatId(int flatId)
        {
            return (_context.FlatOwner.Where(a => a.FlatId == flatId).FirstOrDefault());
        }

        public Shared.FlatOwner GetFlatOwnerByFlatNo(string flatNo)
        {
            return (_context.FlatOwner.Where(a => a.FlatNo == flatNo).FirstOrDefault());
        }

        public List<Shared.FlatOwner> GetFlatOwners()
        {
            return (_context.FlatOwner.ToList());
        }
    }
}
