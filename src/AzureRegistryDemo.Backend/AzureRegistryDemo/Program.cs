using AzureRegistryDemo.DataContexts;
using AzureRegistryDemo.Extensions;
using AzureRegistryDemo.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Environment.GetEnvironmentVariable("DbConnectionString")));

var app = builder.Build();

// Apply pending migrations to database
var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);
await serviceScopeFactory.MigrateAsync<AppDbContext>();

// Add SeedData
var serviceScope = app.Services.CreateScope();
await serviceScope.ServiceProvider.InitializeSeedAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();