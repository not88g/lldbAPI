using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lldbAPI.Data;

namespace lldbAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Send(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }
    }
}
