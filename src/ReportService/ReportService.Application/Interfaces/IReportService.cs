namespace ReportService.Application.Interfaces;

public interface IReportService
{
    Task<Guid> CreateReportAsync();
    Task<ReportDto> GetReportDetailsAsync(Guid id);
    Task<List<ReportDto>> GetAllReportsAsync();
}