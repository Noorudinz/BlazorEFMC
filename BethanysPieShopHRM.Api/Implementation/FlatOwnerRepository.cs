using BethanysPieShopHRM.Api.Models;
using BethanysPieShopHRM.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Api.Implementation
{
    public class FlatOwnerRepository: IFlatOwner
    {
        private readonly AppDbContext _context;
        public FlatOwnerRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public CommonResponse AddFlatOwner(FlatOwner flatOwner)
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

        public FlatOwner GetFlatOwnerByFlatNo(string flatNo)
        {
            return (_context.FlatOwner.Where(a => a.FlatNo == flatNo).FirstOrDefault());
        }

        public List<FlatOwner> GetFlatOwners()
        {
            return (_context.FlatOwner.ToList());
        }
    }
}
