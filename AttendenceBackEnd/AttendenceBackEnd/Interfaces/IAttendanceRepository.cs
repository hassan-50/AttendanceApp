using AttendenceBackEnd.Models;
using AttendenceBackEnd.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance> CheckAttendanceDay(DateTime date , int id);
        Task<bool> AddAttendance(int id , double time ,DateTime date);
        Task<bool> AddAttendanceExistDay(Attendance attendanceDay, double time);
        Task<double> IntervalSumAttendance(int id, DateTime from , DateTime to);
        Task<double> SpecificSumAttendance(int id, DateTime date);
        Task<List<AttendanceDto>> CertainDurationAttendance(int id, DateTime from , DateTime to);
    }
}
