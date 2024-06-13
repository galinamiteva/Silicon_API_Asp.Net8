using Infrastructure.Contexts;
using Infrastructure.Factory;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Silicon_ASP_NET_API.Dtos;
using Silicon_ASP_NET_API.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Silicon_ASP_NET_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(DataContext context, IConfiguration configuration) : ControllerBase
{
    private readonly DataContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegistrationForm form)
    {
        if (ModelState.IsValid)
        {

            if (!await _context.Users.AnyAsync(x => x.Email == form.Email))
            {
                _context.Users.Add(UserFactory.Create(form));
                await _context.SaveChangesAsync();
                return Created("", null);

            }
            return Conflict();
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLoginForm form)
    {
        if (ModelState.IsValid)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == form.Email);

            if (userEntity != null && PasswordHasher.ValidateSecurePassword(form.Password, userEntity.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
                        new Claim(ClaimTypes.Email, userEntity.Email),
                        new Claim(ClaimTypes.Name, userEntity.Email),
                        new Claim(ClaimTypes.Role, "User")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        return BadRequest();
    }

    [UseApiKey]
    [HttpPost]
    [Route("token")]

    public IActionResult GetToken(UserLoginForm form)
    {
        if (ModelState.IsValid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, form.Email),
                    new Claim(ClaimTypes.Name, form.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        return Unauthorized();
    }

}
