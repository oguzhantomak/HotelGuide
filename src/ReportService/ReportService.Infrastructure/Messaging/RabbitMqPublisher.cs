using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

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

    public async Task PublishAsync(string queueName, Guid messageId)
    {
        // EN: Create a connection and channel.
        // TR: Bir bağlantı ve kanal oluştur.
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        // EN: Declare the queue to ensure it exists.
        // TR: Kuyruğun var olduğundan emin olmak için kuyruğu tanımlayın.
        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        // EN: Serialize the message and publish it to the queue.
        // TR: Mesajı serileştir ve kuyruğa yayınla.
        var message = JsonSerializer.Serialize(new { ReportId = messageId });
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);

        await Task.CompletedTask;
    }
}