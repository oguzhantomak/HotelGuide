namespace ReportService.Application.Dtos;


/// <summary>
/// EN: Data Transfer Object for Report.
/// TR: Rapor için veri transfer nesnesi.
/// </summary>
public class ReportDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public List<LocationStatisticDto> Statistics { get; set; } = new();
}