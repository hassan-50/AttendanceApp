using AttendenceBackEnd.Models;
using AttendenceBackEnd.Requests;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUser(AppUser user , string password);
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserByEmail(string email);
        Task<bool> UserCheckPassword(AppUser user, string password);
        Task<bool> CheckUser(AppUser user);
        string GenerateJWT(AppUser user , string role);
        //Task AppendToken(string token);
        Task<IdentityResult> ChangePassword(AppUser user , string oldpassword , string newpassword);
        Task<List<AppUser>> GetAllUsers();
        Task<bool> DisableUser(string email);
        Task<bool> EnableUser(string email);
        Task<IdentityResult> AddRole(string role);
        Task<bool> RoleExists(string role);
        Task<IdentityResult> AddRoleToUser(AppUser user , string role);
        Task<string> GetRole(AppUser user);
        Task<List<string>> GetRoles();
        
    }
}
