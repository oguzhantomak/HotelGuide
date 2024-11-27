namespace ReportService.Infrastructure.Repositories;


/// <summary>
/// EN: Interface for managing report data.
/// TR: Rapor verilerini yönetmek için arayüz.
/// </summary>
public interface IReportRepository
{
    Task AddAsync(Report report);
    Task<Report> GetByIdAsync(Guid id);
    Task<List<Report>> GetAllAsync();
    Task UpdateAsync(Report report);
}