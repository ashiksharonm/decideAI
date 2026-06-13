using DecideAI.API.Data;
using DecideAI.API.Plugins;
using DecideAI.API.Services;
using DecideAI.Core.Interfaces;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb") ?? "mongodb://admin:password@localhost:27017";
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase("DecideAI");

builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

// Register Core Services
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<FinanceService>();
builder.Services.AddSingleton<PortfolioDataPlugin>();
builder.Services.AddSingleton<ICopilotService, CopilotService>();
builder.Services.AddTransient<MongoSeeder>();

var app = builder.Build();

// Seed Database on Startup
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<MongoSeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

