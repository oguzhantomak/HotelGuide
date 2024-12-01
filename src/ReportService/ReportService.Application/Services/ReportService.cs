namespace ReportService.Application.Services;

/// <summary>
/// EN: Implements report management services.
/// TR: Rapor yönetim servislerini uygular.
/// </summary>
public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IMessageQueuePublisher _queuePublisher;

    public ReportService(IReportRepository reportRepository, IMessageQueuePublisher queuePublisher)
    {
        _reportRepository = reportRepository;
        _queuePublisher = queuePublisher;
    }

    public async Task<Guid> CreateReportAsync()
    {
        var report = new Report();
        await _reportRepository.AddAsync(report);

        var eventMessage = new
        {
            ReportId = report.Id,
            Timestamp = DateTime.UtcNow
        };

        _queuePublisher.Publish("hello", eventMessage);

        return report.Id;
    }

    public async Task<ReportDto> GetReportDetailsAsync(Guid reportId)
    {
        var report = await _reportRepository.GetByIdAsync(reportId);
        if (report == null) throw new ApplicationException(ExceptionMessages.ReportNotFound);

        return new ReportDto
        {
            Id = report.Id,
            CreatedAt = report.CreatedAt,
            Status = report.Status.ToString(),
            Statistics = report.Statistics.Select(s => new LocationStatisticDto
            {
                Location = s.Location,
                HotelCount = s.HotelCount,
                ContactInformationCount = s.ContactInformationCount,
                ResponsiblePersonCount = s.ResponsiblePersonCount
            }).ToList()
        };
    }

    public async Task<List<ReportDto>> GetAllReportsAsync()
    {
        var reports = await _reportRepository.GetAllAsync();
        return reports.Select(r => new ReportDto
        {
            Id = r.Id,
            CreatedAt = r.CreatedAt,
            Status = r.Status.ToString(),
            Statistics = r.Statistics.Select(s => new LocationStatisticDto
            {
                Location = s.Location,
                HotelCount = s.HotelCount,
                ContactInformationCount = s.ContactInformationCount,
                ResponsiblePersonCount = s.ResponsiblePersonCount
            }).ToList()
        }).ToList();
    }

    public async Task ProcessReportAsync(Guid reportId, List<HotelStatisticDto> hotelStatistics)
    {
        var report = await _reportRepository.GetByIdAsync(reportId);
        if (report == null) throw new Exception("Report not found");

        foreach (var stat in hotelStatistics)
        {
            var locationStatistic = new LocationStatistic(stat.Location, stat.HotelCount, stat.ContactInformationCount, stat.ResponsiblePersonCount);
            report.AddStatistic(locationStatistic);
        }

        report.MarkAsCompleted();

        await _reportRepository.UpdateAsync(report);
    }
}