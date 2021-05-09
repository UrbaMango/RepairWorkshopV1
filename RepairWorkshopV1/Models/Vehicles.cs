using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class Vehicles
    {
        public Vehicles()
        {
            Visits = new HashSet<Visits>();
        }

        public decimal VehicleId { get; set; }
        public string Make { get; set; }
        public string Vin { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string LicensePlate { get; set; }
        public decimal ClientId { get; set; }

        public virtual Clients Client { get; set; }
        public virtual ICollection<Visits> Visits { get; set; }
    }
}
