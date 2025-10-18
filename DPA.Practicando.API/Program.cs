using DPA.Practicando.DOMAIN.Infrastructure.Data;
using DPA.Practicando.DOMAIN.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var _configuration = builder.Configuration;
var _connectionString = _configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ReservasDeportivasContext>(options =>
{
    options.UseSqlServer(_connectionString);
});
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
