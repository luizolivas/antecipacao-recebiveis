using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Infrastructure.Data;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using AntecipacaoRecebiveis.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AntecipacaoDb"));

builder.Services.AddScoped<INfeService, NfeService>();
builder.Services.AddScoped<INfeRepository, NfeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
