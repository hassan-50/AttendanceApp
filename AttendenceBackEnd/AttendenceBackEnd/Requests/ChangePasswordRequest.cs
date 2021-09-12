using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Requests
{
    public class ChangePasswordRequest
    {
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }
}
