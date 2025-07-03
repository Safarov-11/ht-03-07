using Domain.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationService service) : ControllerBase
{
    [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await service.RegisterAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await service.LoginAsync(dto);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }
}
