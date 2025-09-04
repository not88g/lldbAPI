using Microsoft.EntityFrameworkCore;
using lldbAPI.Models;

namespace lldbAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>(); // 🔥 добавлено
}
