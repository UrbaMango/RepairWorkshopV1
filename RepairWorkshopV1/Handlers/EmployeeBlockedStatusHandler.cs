using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepairWorkshopV1.Helpers;
using RepairWorkshopV1.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace RepairWorkshopV1.Handlers
{
    public class EmployeeBlockedStatusHandler : AuthorizationHandler<EmployeeStatusRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeStatusRequirement requirement)
        {
            var claim = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer);
            if (!context.User.HasClaim(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer))
            {
                return Task.CompletedTask;
            }

            string value = context.User.FindFirst(c => c.Type == "IsBlocked" && c.Issuer == TokenHelper.Issuer).Value;
            var customerBlockedStatus = Convert.ToBoolean(value);

            if (customerBlockedStatus == requirement.IsBlocked)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
