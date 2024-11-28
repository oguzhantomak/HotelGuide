namespace ReportService.Application.Dtos;

/// <summary>
/// EN: Data Transfer Object for LocationStatistic.
/// TR: LocationStatistic için veri transfer nesnesi.
/// </summary>
public class LocationStatisticDto
{
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneNumberCount { get; set; }
}