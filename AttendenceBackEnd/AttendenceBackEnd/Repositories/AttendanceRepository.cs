using AttendenceBackEnd.Data;
using AttendenceBackEnd.Interfaces;
using AttendenceBackEnd.Models;
using AttendenceBackEnd.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
      private readonly ApiDbContext ApiDbContext;

        public AttendanceRepository(ApiDbContext apiDbContext)
        {
            this.ApiDbContext = apiDbContext;
        }

        public async Task<Attendance> CheckAttendanceDay(DateTime date , int id)
        {
            return await ApiDbContext.Attendance.FirstOrDefaultAsync(x => x.AttendanceDate.Date == date.Date && x.AppUserId == id);
        }
        public async Task<bool> AddAttendance(int id, double time, DateTime date)
        {
            Attendance at = new Attendance
            {
                TotalTime = time,
                AppUserId = id,
                AttendanceDate = date
            };
            await ApiDbContext.Attendance.AddAsync(at);
            return await ApiDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddAttendanceExistDay(Attendance attendanceDay, double time)
        {
            attendanceDay.TotalTime += time;
            return await ApiDbContext.SaveChangesAsync() > 0;
        }
        public async Task<double> IntervalSumAttendance(int id, DateTime from, DateTime to)
        {
           return await ApiDbContext.Attendance.Where(x => x.AppUserId == id && x.AttendanceDate.Date >= from.Date && x.AttendanceDate.Date <= to.Date).SumAsync(x => x.TotalTime);
        }
        public async Task<double> SpecificSumAttendance(int id, DateTime date)
        {
           return await ApiDbContext.Attendance.Where(x => x.AppUserId == id && x.AttendanceDate.Date == date.Date).SumAsync(x => x.TotalTime);
        }
        public async Task<List<AttendanceDto>> CertainDurationAttendance(int id, DateTime from, DateTime to)
        {
           return await ApiDbContext.Attendance.Where(x => x.AppUserId == id && x.AttendanceDate.Date >= from.Date && x.AttendanceDate.Date <= to.Date).Select(x => new AttendanceDto(x.AttendanceDate, Math.Round(x.TotalTime, 1), x.AttendanceDate.DayOfWeek.ToString())).ToListAsync();
        }

    }
}
