namespace ReportService.Infrastructure.Messaging;

/// <summary>
/// EN: RabbitMQ implementation for publishing messages to a queue.
/// TR: Mesaj kuyruğuna mesaj yayınlamak için RabbitMQ implementasyonu.
/// </summary>
public class RabbitMqPublisher : IMessageQueuePublisher
{
    private readonly IConnectionFactory _connectionFactory;

    public RabbitMqPublisher(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task Publish(string queueName, object message)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
    }

    public async Task PublishAsync(string queueName, Guid messageId)
    {
    }
}