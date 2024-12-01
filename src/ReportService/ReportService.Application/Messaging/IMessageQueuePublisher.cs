namespace ReportService.Application.Messaging;

public interface IMessageQueuePublisher
{
    Task Publish(string queueName, object message);
    Task PublishAsync(string queueName, Guid messageId);
}