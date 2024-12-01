namespace ReportService.Infrastructure.Messaging;

public class Consumer : IConsumer
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IHotelManagementClient _hotelManagementClient;
    private readonly IReportService _reportService;


    public Consumer(IConnectionFactory connectionFactory, IHotelManagementClient hotelManagementClient, IReportService reportService)
    {
        _connectionFactory = connectionFactory;
        _hotelManagementClient = hotelManagementClient;
        _reportService = reportService;
    }

    public async Task StartConsuming()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        //TODO: Kalkacak
        //await channel.BasicConsumeAsync(
        //    queue: "hello",
        //    autoAck: true,
        //    consumer: consumer
        //);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = await JsonSerializer.DeserializeAsync<TestModel>(new MemoryStream(body));

            Guid reportId = message.ReportId;

            var hotelStats = await _hotelManagementClient.GetStatsAsync();

            await _reportService.ProcessReportAsync(reportId, hotelStats);

            //TODO: Başarılı ise autoAck change
            await Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(queue: "hello", autoAck: true, consumer: consumer);
    }

    /// <summary>
    /// EN: Background service to consume messages from RabbitMQ.
    /// TR: RabbitMQ'dan mesajları tüketmek için arka plan servisi.
    /// </summary>
    public class BackgroundConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var consumer = scope.ServiceProvider.GetRequiredService<IConsumer>();
                    await consumer.StartConsuming();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    //TODO: Düzgün bir modele çevir
    public class TestModel
    {
        public Guid ReportId { get; set; }
    }
}
