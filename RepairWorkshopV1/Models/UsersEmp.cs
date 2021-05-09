using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class UsersEmp
    {
        public UsersEmp()
        {
            Employees = new HashSet<Employees>();
        }

        public decimal UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool Active { get; set; }
        public bool Blocked { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
