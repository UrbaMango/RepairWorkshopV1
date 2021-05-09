using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepairWorkshopV1.Requests;
using RepairWorkshopV1.Responses;

namespace RepairWorkshopV1.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginEmp(LoginRequest loginRequest);
        Task<LoginResponse> LoginClient(LoginRequest loginRequest);
    }
}
