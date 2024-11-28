namespace HotelManagementService.Application.Interfaces;

/// <summary>
/// EN: Service interface for managing hotels.
/// TR: Otelleri yönetmek için servis arayüzü.
/// </summary>
public interface IHotelService
{
    Task<Guid> CreateHotelAsync(HotelDto hotelDto);
    Task AddContactInformationAsync(Guid hotelId, ContactInformationDto contactInformationDto);
    Task AddResponsiblePersonAsync(Guid hotelId, ResponsiblePersonDto responsiblePersonDto);
    Task UpdateHotelAddressAsync(Guid hotelId, string street, string city, string country);
    Task<HotelDto> GetHotelDetailsAsync(Guid hotelId);
    Task DeleteHotelAsync(Guid hotelId);
    Task RemoveContactInformationAsync(Guid hotelId, Guid contactId);
}