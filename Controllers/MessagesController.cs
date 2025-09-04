using Microsoft.AspNetCore.Mvc;
using lldbAPI.Data;
using lldbAPI.Models;

namespace lldbAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MessagesController(AppDbContext context)
    {
        _context = context;
    }

    // получить все сообщения
    [HttpGet]
    public IActionResult GetMessages()
    {
        var messages = _context.Messages
            .OrderByDescending(m => m.Timestamp)
            .Take(50) // последние 50 сообщений
            .ToList();

        return Ok(messages);
    }

    // отправить сообщение
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] Message msgDto)
    {
        var message = new Message
        {
            Sender = msgDto.Sender,
            Content = msgDto.Content,
            Timestamp = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return Ok("Message sent");
    }
}
