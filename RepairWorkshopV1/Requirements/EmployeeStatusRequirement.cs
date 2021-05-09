using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairWorkshopV1.Requirements
{
    public class EmployeeStatusRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; }
        public EmployeeStatusRequirement (bool isBlocked)
        {
            IsBlocked = isBlocked;
        }
    }
}
