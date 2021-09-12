using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Requests
{
    public class AttendanceRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
