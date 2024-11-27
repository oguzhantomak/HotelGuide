using HotelManagementService.Application.Dtos;

namespace HotelManagementService.Application.Services;

/// <summary>
/// EN: Implements hotel management services.
/// TR: Otel yönetim servislerini uygular.
/// </summary>
public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Guid> CreateHotelAsync(HotelDto hotelDto)
    {
        var address = new Address(hotelDto.Street, hotelDto.City, hotelDto.Country);
        var hotel = new Hotel(hotelDto.Name, address);

        foreach (var contact in hotelDto.ContactInformation)
        {
            hotel.AddContactInformation(new ContactInformation(ContactType.FromString(contact.Type), contact.Content));
        }

        foreach (var person in hotelDto.ResponsiblePeople)
        {
            hotel.AddResponsiblePerson(new ResponsiblePerson(person.FirstName, person.LastName));
        }

        await _hotelRepository.AddAsync(hotel);
        return hotel.Id;
    }

    public async Task AddContactInformationAsync(Guid hotelId, ContactInformationDto contactInformationDto)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);
        if (hotel == null) throw new ApplicationException("Hotel not found.");

        hotel.AddContactInformation(new ContactInformation(ContactType.FromString(contactInformationDto.Type), contactInformationDto.Content));


        await _hotelRepository.UpdateAsync(hotel);
    }

    public async Task AddResponsiblePersonAsync(Guid hotelId, ResponsiblePersonDto responsiblePersonDto)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);
        if (hotel == null) throw new ApplicationException("Hotel not found.");

        hotel.AddResponsiblePerson(new ResponsiblePerson(responsiblePersonDto.FirstName, responsiblePersonDto.LastName));
        await _hotelRepository.UpdateAsync(hotel);
    }

    public async Task UpdateHotelAddressAsync(Guid hotelId, string street, string city, string country)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);
        if (hotel == null) throw new ApplicationException("Hotel not found.");

        hotel.UpdateAddress(new Address(street, city, country));
        await _hotelRepository.UpdateAsync(hotel);
    }

    public async Task<HotelDto> GetHotelDetailsAsync(Guid hotelId)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);
        if (hotel == null) throw new ApplicationException("Hotel not found.");

        return new HotelDto
        {
            Name = hotel.Name,
            Street = hotel.Address.Street,
            City = hotel.Address.City,
            Country = hotel.Address.Country,
            ContactInformation = hotel.ContactInformation.Select(c => new ContactInformationDto
            {
                Type = c.Type.ToString(),
                Content = c.Content
            }).ToList(),
            ResponsiblePeople = hotel.ResponsiblePeople.Select(r => new ResponsiblePersonDto
            {
                FirstName = r.FirstName,
                LastName = r.LastName
            }).ToList()
        };
    }

    public async Task DeleteHotelAsync(Guid hotelId)
    {
        await _hotelRepository.DeleteAsync(hotelId);
    }
}
