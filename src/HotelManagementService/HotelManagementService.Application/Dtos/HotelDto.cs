namespace HotelManagementService.Application.Dtos;

/// <summary>
/// EN: Data Transfer Object for a hotel.
/// TR: Otel için veri transfer nesnesi.
/// </summary>
public class HotelDto
{
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<ContactInformationDto> ContactInformation { get; set; } = new();
    public List<ResponsiblePersonDto> ResponsiblePeople { get; set; } = new();
}