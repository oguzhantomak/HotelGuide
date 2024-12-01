namespace ReportService.Infrastructure.Messaging;

public interface IConsumer
{
    Task StartConsuming();
}