using MongoDB.Bson.Serialization.Attributes;
namespace ReportService.Domain.Entities;


/// <summary>
/// EN: Represents the aggregate root for Report.
/// TR: Rapor için aggregate root'u temsil eder.
/// </summary>
public class Report : IAggregateRoot
{
    [BsonId]
    public Guid Id { get; private set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; private set; }

    [BsonElement("status")]
    public ReportStatus Status { get; private set; }

    [BsonElement("statistics")]
    public List<LocationStatistic> Statistics { get; set; } = new();

    // Domain Events
    private readonly List<ReportCreatedEvent> _domainEvents = new();
    public IReadOnlyCollection<ReportCreatedEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Report()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = ReportStatus.Preparing;

        // Trigger domain event
        _domainEvents.Add(new ReportCreatedEvent(Id));
    }

    public void AddStatistic(LocationStatistic statistic)
    {
        if (statistic == null) throw new DomainException(ExceptionMessages.StatisticCannotBeNull);
        Statistics.Add(statistic);
    }

    public void MarkAsCompleted()
    {
        Status = ReportStatus.Completed;
    }
}