using server.Middleware;
using Wood.Application;
using Wood.Infrastructure;
using Wood.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ── Layers ─────────────────────────────────────────────────────────────────
builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection"));

// ── API ────────────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "ЛісоПром Рівне API",
        Version = "v1",
        Description = "API для управління товарами та замовленнями пиломатеріалів"
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ── Seed ───────────────────────────────────────────────────────────────────
using ( var scope = app.Services.CreateScope() )
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(db);
}

// ── Middleware ─────────────────────────────────────────────────────────────
app.UseMiddleware<ExceptionMiddleware>();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
