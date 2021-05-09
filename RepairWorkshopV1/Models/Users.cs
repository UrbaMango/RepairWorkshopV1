using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class Users
    {
        public Users()
        {
            Clients = new HashSet<Clients>();
        }

        public decimal UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool Blocked { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
    }
}
