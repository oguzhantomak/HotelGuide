using System.Text.Json.Serialization;

namespace ReportService.Application.Dtos;

public class HotelStatisticDto
{
    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("hotelCount")]
    public int HotelCount { get; set; }

    [JsonPropertyName("contactInformationCount")]
    public int ContactInformationCount { get; set; }

    [JsonPropertyName("responsiblePersonCount")]
    public int ResponsiblePersonCount { get; set; }

}