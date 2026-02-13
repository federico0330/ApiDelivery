using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Application.Services;
using MyStore.Infrastructure.Commands;
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
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IDishCommand, DishCommand>();
builder.Services.AddScoped<IDishQuery, DishQuery>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

