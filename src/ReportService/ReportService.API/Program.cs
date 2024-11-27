using RabbitMQ.Client;
using ReportService.Application.Interfaces;
using ReportService.Infrastructure.Messaging;
using ReportService.Infrastructure.Repositories;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MongoDB configuration
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

// RabbitMQ configuration
builder.Services.AddSingleton<IConnectionFactory>(_ =>
{
    var factory = new ConnectionFactory
    {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
    };
    return factory;
});

// Dependency Injection
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService.Application.Services.ReportService>();
builder.Services.AddScoped<IMessageQueuePublisher, RabbitMqPublisher>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
