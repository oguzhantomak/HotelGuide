namespace HotelManagementService.Application.Dtos;

/// <summary>
/// EN: Data Transfer Object for contact information.
/// TR: İletişim bilgileri için veri transfer nesnesi.
/// </summary>
public class ContactInformationDto
{
    public string Type { get; set; } // Phone, Email
    public string Content { get; set; }
}