// -------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using System.Reflection;
using System.Text.Json.Serialization;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Policy.Api.Extensions;
using Serilog;
using SharedLibrary.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

Log.Information("Starting up");

builder.Host.UseSerilog();

builder.Host.UseMetricsWebTracking()
    .UseMetrics(options =>
    {
        options.EndpointOptions = endpointsOptions =>
        {
            endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
            endpointsOptions.EnvironmentInfoEndpointEnabled = false;
        };
    });

builder.WebHost.UseKestrel(options =>
{
    options.AddServerHeader = false;
});

// Add services to the container.
var services = builder.Services;

services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

services.AddMetrics();

services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        var enumConverter = new JsonStringEnumConverter();
        options.JsonSerializerOptions.Converters.Add(enumConverter);
    });

services.AddRouting(options => options.LowercaseUrls = true);


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Policy.Api", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();
});

services.AddApiHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Policy.Api v1"));
}

app.UseRouting();

app.UseApiHealthChecks();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
