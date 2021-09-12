using AttendenceBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
