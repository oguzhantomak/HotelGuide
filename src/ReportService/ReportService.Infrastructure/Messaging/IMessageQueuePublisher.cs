namespace ReportService.Infrastructure.Messaging;

public interface IMessageQueuePublisher
{
    Task PublishAsync(string queueName, Guid messageId);
}