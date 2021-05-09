using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepairWorkshopV1.Interfaces;
using RepairWorkshopV1.Requests;
using Microsoft.AspNetCore.Authorization;

namespace RepairWorkshopV1.Controllers
{
    [Route("client/")]
    [ApiController]
    public class LoginClientController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginClientController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Missing login details");
            }

            var loginResponse = await loginService.LoginClient(loginRequest);

            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }

            return Ok(loginResponse);
        }
        [HttpGet]
        [Authorize]
        [Route("authorized")]
        public bool Authorized()
        {
            return true;
        }
    }
}
