using Amazon.SimpleSystemsManagement;
using Autofac.Extensions.DependencyInjection;
using CharityFundRaiserApp.Api.Models;
using CharityFundraiserApp.ApplicationDatabase.Config;
using CharityFundraiserApp.ApplicationDatabase.EfEnhancements;
using CharityFundraiserApp.ApplicationDatabase.Models;
using CharityFundraiserApp.Common.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var ssmRootPath = Environment.GetEnvironmentVariable("SsmParameterStoreRoot");
if (string.IsNullOrEmpty(ssmRootPath))
{
    throw new ArgumentNullException("SsmParameterStoreRoot",
        "The SSM parameter store root path is not configured.");
}

Console.WriteLine($"Adding SSM parameters from '{ssmRootPath}'");
builder.Configuration.AddCharityFundraiserParameterStore();
builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<StravaCredentials>(builder.Configuration.GetSection("Strava"));

var applicationDatabaseSettings = builder.Configuration.GetSection(ApplicationDatabaseSettings.SectionName)
    .Get<ApplicationDatabaseSettings>();

builder.Services.AddApplicationContext(applicationDatabaseSettings);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSService<IAmazonSimpleSystemsManagement>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/app-title", () =>
    {
        return "Supporting Forever Stars.";
    })
    .WithName("GetAppTitle")
    .WithOpenApi();

app.Run();