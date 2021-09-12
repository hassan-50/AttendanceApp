using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Models
{
    public class Attendance
    {

        public int Id { get; set; }
        [Required]
        public DateTime AttendanceDate { get; set; }
        [Required]
        public double TotalTime { get; set; }
        [Required]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
