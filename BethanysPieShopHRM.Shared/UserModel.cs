using System;
using System.Collections.Generic;
using System.Text;

namespace BethanysPieShopHRM.Shared
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<RolesVM> Roles { get; set; }

    }

    public class UserRoleVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }     
        public List<RolesSelection> RolesSelections { get; set; }
    }

    public class RolesSelection
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
