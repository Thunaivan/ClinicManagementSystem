using Clinic.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Clinic.Application.Interfaces;
using Clinic.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// add service s to the controllers
builder.Services.AddControllers();

builder.Services.AddScoped<IPatientService, PatientService>();
// Register enttity framework core
builder.Services.AddDbContext<ClinicDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Clinic API v1");
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



