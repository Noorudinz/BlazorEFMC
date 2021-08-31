using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public interface IRolesDataService
    {
        Task<IEnumerable<RolesVM>> GetAllRoles();
        Task<RolesVM> GetRoleDetails(string roleId);
        Task<RolesResultVM> AddRoleOrEdit(RolesVM role);
        Task<RolesResultVM> DeleteRole(RolesVM roleId);
    }
}
