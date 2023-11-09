using CleanHouse.Service.Interfaces.User;
using CleanHouse.Service.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace CleanHouse.Api.Controllers.Users
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]

        public async Task<IActionResult> PostAsync(LoginDto dto)
        {
            var token = await _authService.AuthenticateAsync(dto);

            return Ok(token);
        }
    }
}
