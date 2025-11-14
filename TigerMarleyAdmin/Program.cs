using Microsoft.EntityFrameworkCore;
using TigerMarleyAdmin.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middlewares
app.UseCors("AllowReactAll");

// ALWAYS enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// REQUIRED FOR AZURE LINUX
app.Urls.Clear();
app.Urls.Add("http://0.0.0.0:8080");

app.UseAuthorization();
app.MapControllers();

app.Run();
