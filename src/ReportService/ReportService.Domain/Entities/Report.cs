namespace ReportService.Domain.Entities;


/// <summary>
/// EN: Represents the aggregate root for Report.
/// TR: Rapor için aggregate root'u temsil eder.
/// </summary>
public class Report : IAggregateRoot
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ReportStatus Status { get; private set; }
    private readonly List<LocationStatistic> _statistics = new();
    public IReadOnlyCollection<LocationStatistic> Statistics => _statistics.AsReadOnly();

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
        if (statistic == null) throw new DomainException("Statistic cannot be null.");
        _statistics.Add(statistic);
    }

    public void MarkAsCompleted()
    {
        Status = ReportStatus.Completed;
    }
}