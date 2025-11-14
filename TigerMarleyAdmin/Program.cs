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
        policy.WithOrigins("http://localhost:3000") // Your frontend URL
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

// Enable Swagger ALWAYS (Azure is Production, so this must run)
app.UseSwagger();
app.UseSwaggerUI();

// REQUIRED FOR AZURE LINUX APP SERVICE
app.Urls.Clear();
app.Urls.Add("http://0.0.0.0:8080");

app.UseAuthorization();

// Map controllers
app.MapControllers();

// ROOT endpoint (to fix 404 on "/")
app.MapGet("/", () => "API is running ✔ Azure OK");

// Start application
app.Run();

