using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
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
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result)
                return BadRequest(new
                {
                    message = "Username already exists."
                });

            return Ok(new
            {
                message = "Register successful."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });

            return Ok(new
            {
                message = "Login successful.",
                token = token
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var tokenResponse = await _authService.RefreshTokenAsync(refreshToken);

            if (tokenResponse == null)
                return Unauthorized(new
                {
                    message = "Invalid or expired refresh token."
                });

            return Ok(new
            {
                message = "Token refreshed successfully.",
                token = tokenResponse
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            var result = await _authService.LogoutAsync(refreshToken);

            if (!result)
                return BadRequest(new
                {
                    message = "Invalid or already revoked refresh token."
                });

            return Ok(new
            {
                message = "Logout successful."
            });
        }
    }
}
