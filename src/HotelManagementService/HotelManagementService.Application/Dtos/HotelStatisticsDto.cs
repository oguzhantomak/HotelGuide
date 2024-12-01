namespace HotelManagementService.Application.Dtos;

public class HotelStatisticsDto
{
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int ContactInformationCount { get; set; }
    public int ResponsiblePersonCount { get; set; }
}