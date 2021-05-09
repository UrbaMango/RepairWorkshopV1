using System;
using System.Collections.Generic;

namespace RepairWorkshopV1.Models
{
    public partial class Employees
    {
        public decimal EmployeeId { get; set; }
        public decimal UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string AccountType { get; set; }

        public virtual UsersEmp User { get; set; }
    }
}
