using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendenceBackEnd.Requests
{
    public class UsersDto
    {
        public UsersDto(string fullname , string email , string username  ,string role , int checkede , bool Enablede)
        {
            FullName = fullname;
            Email = email;
            UserName = username;
            Role = role;
            Checked = checkede;
            Enabled = Enablede;
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int Checked { get; set; }
        public bool Enabled { get; set; }

    }
}
