using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepairWorkshopV1.Responses;
using RepairWorkshopV1.Requests;
using RepairWorkshopV1.Interfaces;
using RepairWorkshopV1.Models;
using RepairWorkshopV1.Helpers;

namespace RepairWorkshopV1.Services
{
    public class LoginService : ILoginService
    {
        private readonly RepairWorkshopContext repairWorkshopContext;
        public LoginService(RepairWorkshopContext repairWorkshopContext)
        {
            this.repairWorkshopContext = repairWorkshopContext;
        }
        public async Task<LoginResponse> LoginEmp(LoginRequest loginRequest)
        {
            var employee = repairWorkshopContext.UsersEmp.SingleOrDefault(employee => employee.Active && employee.Username == loginRequest.Username);

            if (employee == null)
            {
                return null;
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, employee.PasswordSalt);
            if(employee.Password != passwordHash)
            {
                return null;
            }
            var token = await Task.Run(() => TokenHelper.GenerateTokenEmp(employee));
            return new LoginResponse { Username = employee.Username, Token = token, Name = "Employee", UserId = employee.UserId };
        }
        public async Task<LoginResponse> LoginClient(LoginRequest loginRequest)
        {
            var employee = repairWorkshopContext.Users.SingleOrDefault(employee => employee.Active && employee.Username == loginRequest.Username);

            if (employee == null)
            {
                return null;
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, employee.PasswordSalt);
            if (employee.Password != passwordHash)
            {
                return null;
            }
            var token = await Task.Run(() => TokenHelper.GenerateTokenClient(employee));
            return new LoginResponse { Username = employee.Username, Token = token, Name = "Client", UserId = employee.UserId };
        }
    }
}
