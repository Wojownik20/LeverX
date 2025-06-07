using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LeverX.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicStore.Shared.Models.JWT_Authentication;


namespace LeverX.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static List<User> _users = new();
    private readonly IConfiguration _configuration;

    public AuthController(IDbConnection dbConnection, IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Register endpoint for users
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        if (_users.Any(u => u.Username == dto.Username))
            return BadRequest("User already exists");

        var user = new User
        {
            Username = dto.Username,
            Password = PasswordHasher.Hash(dto.Password),
            Role = MusicStore.Shared.Models.UserRole.User
        };

        _users.Add(user);
        return Ok("User registered");
    }

    /// <summary>
    /// Login endpoint for users
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _users.FirstOrDefault(u => u.Username == dto.Username);
        if (user is null || !PasswordHasher.Verify(dto.Password, user.Password))
            return Unauthorized("Invalid credentials");

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(10);

        return Ok(new
        {
            accessToken,
            refreshToken
        });
    }

    /// <summary>
    /// Return Users Name if User is authenticated
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("secret")]
    public IActionResult SecretData()
    {
        var username = User.Identity?.Name;
        return Ok($"Access granted for user: {username}");
    }

    /// <summary>
    /// For every User with Id%==0 returns 200 True
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "PremiumOnly")]
    [HttpGet("vip-only")]
    public IActionResult PremiumStuff()
    {
        return Ok("You are a premium user!");
    }
    /// <summary>
    /// Gives info about current User
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("Info")]
    public IActionResult Info()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }

    /// <summary>
    /// Checks Users refresh token
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        var user = _users.FirstOrDefault(u =>
            u.RefreshToken == refreshToken &&
            u.RefreshTokenExpiryTime > DateTime.UtcNow);

        if (user is null)
            return Unauthorized("Invalid or expired refresh token");

        var newAccessToken = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(10);

        return Ok(new
        {
            accessToken = newAccessToken,
            refreshToken = newRefreshToken
        });
    }


    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),

                // Additional Claims
                new Claim("IsPremiumUser", (user.Id % 2 == 0).ToString()), 
                new Claim("Department", "Engineering")
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
