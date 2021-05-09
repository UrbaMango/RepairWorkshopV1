using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class VisitTasks
    {
        public decimal TaskId { get; set; }
        public bool OilChange { get; set; }
        public decimal VisitId { get; set; }
        public string Description { get; set; }
        public decimal? EmployeeId { get; set; }

        public virtual Visits Visit { get; set; }
    }
}
