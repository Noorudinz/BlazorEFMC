﻿using BethanysPieShopHRM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserRoleVM> GetUsersById(string Id);
        Task<RegisterResult> UpdateUserRoles(UserRoleVM userRolesModel);
        Task<RegisterResult> DeleteUser(RegisterModel registerModel);
    }
}
