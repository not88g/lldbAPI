using Microsoft.EntityFrameworkCore;
using lldbAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// строка подключения к Render PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=dpg-d2s5c1mmcj7s73fslto0-a.oregon-postgres.render.com;Database=datalumiabaselink;Username=datalumiabaselink_user;Password=MGeBsOZdrxxTbT1sVDd1oXlKOfvI7qBI;SSL Mode=Require;Trust Server Certificate=true";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
