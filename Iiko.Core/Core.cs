using Iiko.Application.Repositories;
using Iiko.Common.AssemblyMarker;
using Iiko.Common.ExceptionsFilter;
using Iiko.Infrastructure.Database;
using Iiko.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

var connectionString = config.GetConnectionString("URI");

Log.Information("ConnectionString: {@connectionString}", connectionString);

builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

builder.Services
    .AddSerilog()
    .AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(config.GetConnectionString("URI")))
    .AddScoped<IClientRepository, ClientRepository>()
    .AddFluentValidationAutoValidation(fv =>
    {
        fv.DisableDataAnnotationsValidation = true;
    });



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

