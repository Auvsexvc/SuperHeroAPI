global using Microsoft.EntityFrameworkCore;
global using SuperHeroAPI.Data;
global using SuperHeroAPI.Interfaces;
global using SuperHeroAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NLog.Web;
using SuperHeroAPI.Entities;
using SuperHeroAPI.MiddleWare;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.Validators;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<SuperHeroDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();
builder.Services.AddScoped<ISuperPowerService, SuperPowerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<CreateSuperHeroDto>, CreateSuperHeroDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateSuperHeroDto>, UpdateSuperHeroDtoValidator>();
builder.Services.AddScoped<IValidator<CreateSuperPowerDto>, CreateSuperPowerDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateSuperPowerDto>, UpdateSuperPowerDtoValidator>();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();