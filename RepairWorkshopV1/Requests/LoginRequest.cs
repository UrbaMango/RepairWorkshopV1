using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairWorkshopV1.Requests
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
    }
}
