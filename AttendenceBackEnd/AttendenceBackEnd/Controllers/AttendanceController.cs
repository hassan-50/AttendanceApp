using AttendenceBackEnd.Data;
using AttendenceBackEnd.Interfaces;
using AttendenceBackEnd.Models;
using AttendenceBackEnd.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _Attendance;
        private readonly IUserRepository _User;

        public AttendanceController(ApiDbContext ApiDbContext ,IAttendanceRepository Attendance , IUserRepository User)
        {
            this._Attendance = Attendance;
            this._User = User;
        }
        [HttpPost("add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> add (AttendanceRequest dates)
        {            
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);
            TimeSpan mins = dates.To.Subtract(dates.From);
            double tx = (mins.TotalMinutes / 60.0);
            var check = await _Attendance.CheckAttendanceDay(dates.From , id);

            bool result = false;    
            if(check == null){
                 result = await _Attendance.AddAttendance(id, tx, dates.From.Date);
                //return Ok(new LoginResponse(ResponseCode.OK, "Attendance Added successfully", null));
            }
            else 
            {
                result = await _Attendance.AddAttendanceExistDay(check, tx);
            }
                if (result)
                return Ok(new LoginResponse(ResponseCode.OK, "Attendance Added successfully", null));

            return BadRequest(new LoginResponse(ResponseCode.Error, "Something Went Wrong", null));

        }
        [HttpGet("lastWeek")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> lastWeek()
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);
            DateTime DN = DateTime.Now;
            List<AttendanceDto> attendanceDTO = new List<AttendanceDto>();

            while (true)
            {
                var atTemp = await _Attendance.CheckAttendanceDay(DN, id);
                if (atTemp != null && Math.Round(atTemp.TotalTime, 1) >0)
                    attendanceDTO.Add(new AttendanceDto(atTemp.AttendanceDate.Date, Math.Round(atTemp.TotalTime, 1), atTemp.AttendanceDate.DayOfWeek.ToString()));
                DN = DN.AddDays(-1);
                if(DN.DayOfWeek.ToString() == "Friday")
                    break;
            }
            //for (int i = 1;i<=7 ; i++)
            //{
            //    var atTemp = await _Attendance.CheckAttendanceDay(DN, id); 
            //    if (atTemp != null)
            //    attendanceDTO.Add(new AttendanceDto(atTemp.AttendanceDate.Date, Math.Round(atTemp.TotalTime , 1), atTemp.AttendanceDate.DayOfWeek.ToString()));

            //}
            return Ok(new LoginResponse(ResponseCode.OK, "", attendanceDTO));
        }
        [HttpPost("interval")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> interval(IntervalDto inte)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);
            var role = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role).Value;

            var user = await _User.GetUserByEmail(inte.Email); 
            if (user.Id != id && role != "Admin")
                return BadRequest(new LoginResponse(ResponseCode.Error, "Not Authorized", null));

            DateTime DN = DateTime.Now;
            var result = await _Attendance.IntervalSumAttendance(user.Id, inte.From, inte.To);

            return Ok(new LoginResponse(ResponseCode.OK, "", Math.Round(result,1)));
        }
        [HttpPost("specific")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> specific(SpecificDto specific)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);
            //var my = HttpContext.User.Claims.FirstOrDefault(a => a.Type == "email").Value;

            var user = await _User.GetUserByEmail(specific.Email); 
            var role = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role).Value;
            if(user.Id != id && role != "Admin")
                return BadRequest(new LoginResponse(ResponseCode.Error, "Not Authorized", null));

            DateTime DN = DateTime.Now;
            var result = await _Attendance.SpecificSumAttendance(id, specific.SpecificDate);

            return Ok(new LoginResponse(ResponseCode.OK, "", Math.Round(result, 1)));
        }
        [HttpPost("certainDuration")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> certainDuration(IntervalDto inte)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);            

            List<AttendanceDto> attendanceDTO = new List<AttendanceDto>();
            var user = await _User.GetUserByEmail(inte.Email); 
            var role = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role).Value;
            if (user.Id != id && role != "Admin")
                return BadRequest(new LoginResponse(ResponseCode.Error, "Not Authorized", null));

            DateTime DN = DateTime.Now;
            attendanceDTO = await _Attendance.CertainDurationAttendance(id, inte.From, inte.To);

            return Ok(new LoginResponse(ResponseCode.OK, "", attendanceDTO));
        }
        [HttpPost("absent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> absent(IntervalDto inte)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id").Value);
            //var my = HttpContext.User.Claims.FirstOrDefault(a => a.Type == "email").Value;

            List<AttendanceDto> attendanceDTO = new List<AttendanceDto>();
            var user = await _User.GetUserByEmail(inte.Email);
            var role = HttpContext.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role).Value;
            if (user.Id != id && role != "Admin")
                return BadRequest(new LoginResponse(ResponseCode.Error, "Not Authorized", null));

            DateTime DN = inte.From;
            while(DN.Date <= inte.To.Date)
            {
                var check = await _Attendance.CheckAttendanceDay(DN,user.Id);
                if(check == null)
                attendanceDTO.Add(new AttendanceDto(DN, 0, DN.DayOfWeek.ToString()));

                DN = DN.AddDays(1);
            }

            return Ok(new LoginResponse(ResponseCode.OK, "", attendanceDTO));
        }
    }

}
