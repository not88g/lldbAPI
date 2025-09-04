using Microsoft.AspNetCore.Mvc;
using lldbAPI.Data;
using lldbAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace lldbAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User userDto)
    {
        if (_context.Users.Any(u => u.Username == userDto.Username))
            return BadRequest("User already exists.");

        // создаем соль
        var saltBytes = RandomNumberGenerator.GetBytes(16);
        var salt = Convert.ToBase64String(saltBytes);

        // хешируем пароль
        var sha = SHA256.Create();
        var salted = Encoding.UTF8.GetBytes(userDto.PasswordHash + salt);
        var hash = Convert.ToBase64String(sha.ComputeHash(salted));

        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = hash,
            Salt = salt
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User userDto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == userDto.Username);
        if (user == null) return Unauthorized("Invalid credentials");

        var sha = SHA256.Create();
        var salted = Encoding.UTF8.GetBytes(userDto.PasswordHash + user.Salt);
        var hash = Convert.ToBase64String(sha.ComputeHash(salted));

        if (hash != user.PasswordHash)
            return Unauthorized("Invalid credentials");

        return Ok("Login success");
    }
}
