using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Infrastructure.Data;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using AntecipacaoRecebiveis.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Permitir todas as origens (para testes - cuidado em produção!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddControllers();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AntecipacaoDb"));

builder.Services.AddScoped<INfeService, NfeService>();
builder.Services.AddScoped<INfeRepository, NfeRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICreditLimitCalculator, CreditLimitCalculator>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IBuilderJsonCalculateValues, BuilderJsonCalculateValues>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var company = new Company {
        Id = 1,
        Name = "Empresa Teste",
        Cnpj = "00.000.000/0001-00",
        MonthlyBiling = 10000,
        Sector = Sector.PRODUCAO,
        CreditLimit = 5000
    };

    var nfe = new Nfe {
        Id = 1,
        Number = "123456",
        ExpirationDate = DateTime.Now.AddDays(30),
        CompanyId = 1,
        Value = 5000,
        Company = company
    };

    context.Companies.Add(company);
    context.Nfes.Add(nfe);
    await context.SaveChangesAsync();
}

app.Run();


