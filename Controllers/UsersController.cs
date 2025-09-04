using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lldbAPI.Data;

namespace lldbAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }
    }
}
