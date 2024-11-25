using Aiko.Application.Repositories;
using Aiko.Common.ExceptionsFilter;

using Aiko.Infrastructure.Database;
using Aiko.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Services
    .AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(config.GetConnectionString("URI")))
    .AddScoped<IEntityRepository, EntityRepository>()
    .AddValidatorsFromAssemblies(assemblies)
    .AddFluentValidationAutoValidation()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSerilog()
    .AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCustomExceptionHandler();

app.Run();

