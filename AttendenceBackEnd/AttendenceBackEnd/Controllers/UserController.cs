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

namespace AttendenceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _User;
        private readonly IEmailSender _emailSender;

        public UserController(ApiDbContext ApiDbContext, IEmailSender emailSender , IUserRepository User)
        {
            this._User = User;
            _emailSender = emailSender;
        }
        [HttpPost("register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> register(RegisterRequest rr)
        {
            if (!await _User.RoleExists(rr.Role))
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "Role Does Not Exist", null));
            }
            var user = await _User.GetUserByEmail(rr.Email);
            if (user is not null)
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "Email Already Existed", null));
            }
            user = new AppUser
            {
                FullName = rr.FullName,
                UserName = rr.UserName,
                Email = rr.Email,
            };

            var result = await _User.AddUser(user, rr.Password);
            if (result.Succeeded)
            {
                var tempUser = await _User.GetUserByEmail(rr.Email);
                await _User.AddRoleToUser(tempUser, rr.Role);
                return Ok( new LoginResponse(ResponseCode.OK,"User has been registered",null));
            }
            else
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "Server Error",result.Errors.Select(x=> x.Description).ToArray()));
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest rr)
        {
            var user = await _User.GetUserByEmail(rr.Email);
            user.NormalizedEmail = user.Email;
            
            if (user is null)
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "Invalid Authentication", null));
            }
            if (user.LockoutEnabled == false)
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "User Disabled", null));
            }

            var result = await _User.UserCheckPassword(user, rr.Password);
            if (result)
            {
                var role = await _User.GetRole(user);
                var token = _User.GenerateJWT(user, role);
                HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    Secure = false
                });
                if (role == null)
                {
                    role = "";
                }
                var _user = new UsersDto(user.FullName, user.Email, user.UserName, role , user.Checked , user.LockoutEnabled);
                _user.Token = token;              
                return Ok(new LoginResponse(ResponseCode.OK, "", _user));
            }
            else
            {
               return BadRequest( new LoginResponse(ResponseCode.Error, "Invalid Authentication", null));
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            return Ok(new
            {
                message = "success"
            });
        }
        [HttpGet("GetAllUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            List<UsersDto> allUserDTO = new List<UsersDto>();
            var users = await _User.GetAllUsers();
            foreach(var user in users)
            {
                var role = await _User.GetRole(user);
                allUserDTO.Add(new UsersDto(user.FullName, user.Email, user.UserName, role , user.Checked , user.LockoutEnabled));
            }

            return Ok(new LoginResponse(ResponseCode.OK, "", allUserDTO));

        }
        [HttpPut("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changereq )
        {
            if(changereq.oldpassword == changereq.newpassword)
                return BadRequest(new LoginResponse(ResponseCode.Error, "NewPassword Must be diffrent than the old one", null));

            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);
            var us =await  _User.GetUserById(id);
            var result = await _User.ChangePassword(us, changereq.oldpassword, changereq.newpassword);
            if (result.Succeeded)
            {
            if(us.Checked == 0)
                {
                    await _User.CheckUser(us);                    
                }
            
                return Ok(new LoginResponse(ResponseCode.OK, "Password Changed Succesfully", null));
            }

            return BadRequest(new LoginResponse(ResponseCode.Error, "Invalid Authentication", null));

        }
        [HttpPut("disableUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> disableUser(DisableRequest dr)
        {
                if (await _User.DisableUser(dr.email) == true)
                {
                return Ok(new LoginResponse(ResponseCode.OK, "User Disabled Successfully", null));
                }

            return BadRequest(new LoginResponse(ResponseCode.Error, "Something Went Wrong", null));

        }
        [HttpPut("enableUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> enableUser(DisableRequest dr)
        {
            if (await _User.EnableUser(dr.email) == true)
            {               
             return Ok(new LoginResponse(ResponseCode.OK, "User Disabled Successfully", null));
            }

             return BadRequest(new LoginResponse(ResponseCode.Error, "Something Went Wrong", null));

        }
        [HttpGet("GetRoles")]        
        public async Task<IActionResult> GetRoles()
        {            
                var roles =await _User.GetRoles();            

            return Ok(new LoginResponse(ResponseCode.OK, "", roles));

        }
        [HttpPost("AddRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleModel model)
        {
            if (model == null || model.Role == "")
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "Invalid Authentication", null));
            }
            if (await _User.RoleExists(model.Role))
            {
                return BadRequest(new LoginResponse(ResponseCode.Error, "Role Already Exists", null));
            }
            var result = await _User.AddRole(model.Role);
            if (result.Succeeded)
            {
                return Ok(new LoginResponse(ResponseCode.OK, "Role added successfully", null));

            }
            return BadRequest(new LoginResponse(ResponseCode.Error, "Something Went Wrong", null));
        }
    }
}
