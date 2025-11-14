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
        policy.WithOrigins("http://localhost:3000") // adjust when you have prod URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// PostgreSQL connection via ConnectionStrings:DefaultConnection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middlewares
app.UseCors("AllowReactAll");

// Always enable swagger (convenient for testing)
app.UseSwagger();
app.UseSwaggerUI();

// REQUIRED FOR AZURE LINUX APP SERVICE
app.Urls.Clear();
app.Urls.Add("http://0.0.0.0:8080");

app.UseAuthorization();
app.MapControllers();

// Root test endpoint to avoid 404 at "/"
app.MapGet("/", () => Results.Text("API is running ✔ Azure OK"));

// Health-check endpoint (checks DB)
app.MapGet("/health", async (AppDbContext db) =>
{
    try
    {
        // a lightweight DB call
        await db.Database.ExecuteSqlRawAsync("SELECT 1");
        return Results.Ok(new { status = "healthy" });
    }
    catch (Exception ex)
    {
        return Results.Problem(title: "db-error", detail: ex.Message);
    }
});

app.Run();


