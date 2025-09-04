using Microsoft.EntityFrameworkCore;

namespace lldbAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
    }
}
