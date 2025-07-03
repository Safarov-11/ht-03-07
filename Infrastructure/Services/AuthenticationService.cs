using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthenticationService(
    UserManager<IdentityUser> userManager,
    IConfiguration config) : IAuthenticationService
{
    public async Task<IdentityResult> RegisterAsync(RegisterDTO register)
    {
        var user = new IdentityUser { UserName = register.Username };
        var result = await userManager.CreateAsync(user, register.Password);
        return result;
    }

    public async Task<string?> LoginAsync(LoginDTO login)
    {
        var user = await userManager.FindByNameAsync(login.Username);
        if (user == null) return null;

        var result = await userManager.CheckPasswordAsync(user, login.Password);
        return !result
            ? null
            : GenerateJwtToken(user);
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName!)
            };

        var secretKey = config["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

