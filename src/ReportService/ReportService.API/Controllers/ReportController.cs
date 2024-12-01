namespace ReportService.API.Controllers;


/// <summary>
/// EN: API controller for managing reports.
/// TR: Raporları yönetmek için API kontrolcüsü.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReport()
    {
        var reportId = await _reportService.CreateReportAsync();
        return Accepted(new { ReportId = reportId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportDetails(Guid id)
    {
        var report = await _reportService.GetReportDetailsAsync(id);
        if (report == null) return NotFound();
        return Ok(report);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }
}