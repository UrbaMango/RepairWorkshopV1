using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Vehicles = new HashSet<Vehicles>();
        }

        public decimal ClientId { get; set; }
        public decimal UserId { get; set; }
        public string Email { get; set; }
        public bool CompanyAccount { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public decimal PhoneNumber { get; set; }
        public decimal? RegistrationCode { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}
