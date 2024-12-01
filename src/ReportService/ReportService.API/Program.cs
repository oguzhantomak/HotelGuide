var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Load configuration
var configuration = builder.Configuration;

// MongoDB configuration
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// RabbitMQ configuration
var rabbitMqSettings = configuration.GetSection("RabbitMQ");
builder.Services.AddSingleton<IConnectionFactory>(_ =>
    new ConnectionFactory
    {
        HostName = rabbitMqSettings["HostName"],
        Port = int.Parse(rabbitMqSettings["Port"]),
        UserName = rabbitMqSettings["UserName"],
        Password = rabbitMqSettings["Password"]
    });

builder.Services.AddHttpClient<IHotelManagementClient, HotelManagementClient>(client =>
{
    client.BaseAddress = new Uri("http://hotelmanagementservice:5000");
});

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService.Application.Services.ReportService>();
builder.Services.AddScoped<IMessageQueuePublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IConsumer, Consumer>();
builder.Services.AddHostedService<BackgroundConsumerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

 app.Run();
