using Clinic.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Clinic.Application.Interfaces;
using Clinic.Infrastructure.Services;
using Clinic.API.Middleware;


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
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200","http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseGlobalExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();



