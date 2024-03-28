using Microsoft.Extensions.DependencyInjection;
using ResidentApi.BusinessLogic.Manager;
using ResidentApi.BusinessLogic.Repositories;
using ResidentAPI.CustomEnrichers;
using Serilog;
using Serilog.Core;
using System;

var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .AddJsonFile("appsettings.Development.json")
           .Build();

var builder = WebApplication.CreateBuilder(args);

// we can configure serilog via fluent api
//builder.Services.AddSerilog(options =>
//{
//    options.MinimumLevel.Information()
//           .WriteTo.Console(new JsonFormatter(), LogEventLevel.Debug);
//});


//Read from appsettings.json
//builder.Services.AddSerilog(options =>
//{
//    //we can configure serilog from configuration
//    options.ReadFrom.Configuration(configuration);
//});

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

//Add singleton
builder.Services.AddSingleton<HttpRequestAndCorrelationContextEnricher>();

// Add services to the container.
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IResidentManager, ResidentManager>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Sample for enriching using custom enricher
builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
{
    var enricher = serviceProvider.GetRequiredService<HttpRequestAndCorrelationContextEnricher>();
    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.With(enricher) // Register custom enricher
        .WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
