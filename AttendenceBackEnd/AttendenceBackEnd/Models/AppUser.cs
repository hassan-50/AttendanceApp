using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            if (Email != null)
            {
            NormalizedEmail = Email.ToUpper();   

            }
        }
        [Required]
        public string FullName { get; set; }
        public int Checked { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
