namespace ReportService.Domain.ValueObjects;

/// <summary>
/// EN: Represents the status of a report.
/// TR: Bir raporun durumunu temsil eder.
/// </summary>
public class ReportStatus
{
    public static ReportStatus Preparing => new("Preparing");
    public static ReportStatus Completed => new("Completed");

    public string Status { get; private set; }

    private ReportStatus(string status)
    {
        Status = status;
    }

    public override string ToString()
    {
        return Status;
    }
}