using FinancialBuddy.Application.DTOs.Auth;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBuddy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
    }
}
