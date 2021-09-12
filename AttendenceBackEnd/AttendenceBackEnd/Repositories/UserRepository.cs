using AttendenceBackEnd.Data;
using AttendenceBackEnd.Models;
using AttendenceBackEnd.Requests;
using AttendenceBackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutinesProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext ApiDbContext;
        private readonly UserManager<AppUser> UserManager;
        private readonly RoleManager<IdentityRole<int>> RoleManager;
        private readonly JwtGenerator Jwt;
        public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager, JwtGenerator jwt, ApiDbContext apiDbContext)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.Jwt = jwt;
            this.ApiDbContext = apiDbContext;
        }
        public async Task<IdentityResult> AddUser(AppUser user, string password)
        {
            return await UserManager.CreateAsync(user, password);
        }
        public async Task<AppUser> GetUserById(int id)
        {
            return await ApiDbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }
        public async Task<bool> UserCheckPassword(AppUser user, string password)
        {
            return await UserManager.CheckPasswordAsync(user, password);
        }
        public string GenerateJWT(AppUser user, string role)
        {
            return  Jwt.GenerateJwt(user, role);
        }
        //public Task AppendToken(string token)
        //{
        //    return HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions()
        //    {
        //        Expires = DateTime.Now.AddDays(7),
        //        HttpOnly = true,
        //        Secure = false
        //        //IsEssential = false
        //    });
        //}
        public async Task<IdentityResult> ChangePassword(AppUser user, string oldpassword, string newpassword)
        {
            return await UserManager.ChangePasswordAsync(user, oldpassword, newpassword);
        }
        
        public async Task<List<AppUser>> GetAllUsers()
        {
            return await UserManager.Users.ToListAsync();
        }
        public async Task<bool> DisableUser(string email)
        {
            AppUser user = await GetUserByEmail(email);
            if (user.LockoutEnabled == true)
            {
                user.LockoutEnabled = false;
                 var result = await ApiDbContext.SaveChangesAsync() > 0;
                if (result)
                    return true;
            }
            return false;
        }
        public async Task<bool> EnableUser(string email)
        {
            AppUser user = await GetUserByEmail(email);
            if (user.LockoutEnabled == false)
            {
                user.LockoutEnabled = true;
                var result = await ApiDbContext.SaveChangesAsync() > 0;
                if (result)
                    return true;
            }
            return false;
        }
        public async Task<IdentityResult> AddRole(string role)
        {
            var Identityrole = new IdentityRole<int>();
            Identityrole.Name = role;
            return await RoleManager.CreateAsync(Identityrole);
        }
        public async Task<bool> RoleExists(string role)
        {
            return await RoleManager.RoleExistsAsync(role);

        }
        public async Task<IdentityResult> AddRoleToUser(AppUser user, string role)
        {
            return await UserManager.AddToRoleAsync(user, role);
        }
        public async Task<string> GetRole(AppUser user)
        {
            return (await UserManager.GetRolesAsync(user)).FirstOrDefault();
        }
        public async Task<List<string>> GetRoles()
        {
            return await RoleManager.Roles.Select(x => x.Name).ToListAsync();
        }
        public async Task<bool> CheckUser(AppUser user)
        {
            user.Checked = 1;
            var result = await ApiDbContext.SaveChangesAsync() > 0;
            if (result)
                return true;

            return false;
        }
    }
}
