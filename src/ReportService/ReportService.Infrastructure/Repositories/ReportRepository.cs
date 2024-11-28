namespace ReportService.Infrastructure.Repositories;


/// <summary>
/// EN: MongoDB implementation for managing report data.
/// TR: Rapor verilerini yönetmek için MongoDB implementasyonu.
/// </summary>
public class ReportRepository : IReportRepository
{
    private readonly IMongoCollection<Report> _reports;

    public ReportRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("ReportService");
        _reports = database.GetCollection<Report>("Reports");
    }

    public async Task AddAsync(Report report)
    {
        await _reports.InsertOneAsync(report);
    }

    public async Task<Report> GetByIdAsync(Guid id)
    {
        return await _reports.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Report>> GetAllAsync()
    {
        return await _reports.Find(_ => true).ToListAsync();
    }

    public async Task UpdateAsync(Report report)
    {
        await _reports.ReplaceOneAsync(r => r.Id == report.Id, report);
    }
}