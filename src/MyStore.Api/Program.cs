using Microsoft.EntityFrameworkCore;
using MyStore.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//1. Registro servicios de controladores
builder.Services.AddControllers();

// 1. Obtener la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Inyectar el DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
    
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

