using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Requests
{
    public class AttendanceDto
    {
        public AttendanceDto(DateTime AD , double TT , string DN)
        {
            AttendanceDate = AD;
            TotalTime = TT;
            DayName = DN;
        }
        public DateTime AttendanceDate { get; set; }
        public double TotalTime { get; set; }
        public string DayName { get; set; }
    }
}
