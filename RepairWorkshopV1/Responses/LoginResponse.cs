using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairWorkshopV1.Responses
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public decimal UserId { get; set; }

    }
}
