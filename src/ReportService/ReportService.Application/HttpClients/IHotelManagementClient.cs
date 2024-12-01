namespace ReportService.Application.HttpClients;

public interface IHotelManagementClient
{
    Task<List<HotelStatisticDto>> GetStatsAsync();
}