namespace ReportService.Domain.Events;


/// <summary>
/// EN: Event triggered when a report is created.
/// TR: Bir rapor oluşturulduğunda tetiklenen olay.
/// </summary>
public class ReportCreatedEvent
{
    public Guid ReportId { get; }

    public ReportCreatedEvent(Guid reportId)
    {
        ReportId = reportId;
    }
}