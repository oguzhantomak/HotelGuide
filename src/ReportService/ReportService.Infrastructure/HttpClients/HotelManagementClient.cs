namespace ReportService.Infrastructure.HttpClients;

public class HotelManagementClient : IHotelManagementClient
{
    private readonly HttpClient _httpClient;

    public HotelManagementClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://hotelmanagementservice:5000");
    }

    public async Task<List<HotelStatisticDto>> GetStatsAsync()
    {
        var response = await _httpClient.GetAsync("/api/hotel/getstats");
        var responseModel = new List<HotelStatisticDto>();
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        try
        {
            responseModel = JsonSerializer.Deserialize<List<HotelStatisticDto>>(content);
        }
        catch (Exception e)
        {
            //TODO: Log
            Console.WriteLine(e);
            throw;
        }

        return responseModel;
    }
}