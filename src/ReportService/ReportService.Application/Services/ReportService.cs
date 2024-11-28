using ReportService.Application.Constants;

namespace ReportService.Application.Services;

/// <summary>
/// EN: Implements report management services.
/// TR: Rapor yönetim servislerini uygular.
/// </summary>
public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Guid> CreateReportAsync()
    {
        var report = new Report();
        await _reportRepository.AddAsync(report);
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
                PhoneNumberCount = s.PhoneNumberCount
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
                PhoneNumberCount = s.PhoneNumberCount
            }).ToList()
        }).ToList();
    }
}