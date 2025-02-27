using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using ApiEntegrasyon.Models;
using ApiEntegrasyon.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHangfire(configuration => configuration
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddScoped<IntegrationService>();
Connect.setConnectionString(builder.Configuration.GetConnectionString("SednaDB"));
builder.Services.AddHangfireServer();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
