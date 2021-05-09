using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class Visits
    {
        public Visits()
        {
            VisitTasks = new HashSet<VisitTasks>();
        }

        public decimal VisitId { get; set; }
        public decimal ClientId { get; set; }
        public decimal VehicleId { get; set; }
        public decimal Mileage { get; set; }
        public DateTime VisitStartDate { get; set; }
        public DateTime? VisitEndDate { get; set; }
        public string Notes { get; set; }
        public decimal? VisitPrice { get; set; }
        public bool Confirmed { get; set; }
        public string Progress { get; set; }

        public virtual Vehicles Vehicle { get; set; }
        public virtual ICollection<VisitTasks> VisitTasks { get; set; }
    }
}
