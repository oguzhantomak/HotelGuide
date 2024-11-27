namespace HotelManagementService.Domain.Events;

/// <summary>
/// EN: Event triggered when a hotel is created.
/// TR: Bir otel oluşturulduğunda tetiklenen olay.
/// </summary>
public class HotelCreatedEvent
{
    public Guid HotelId { get; }

    public HotelCreatedEvent(Guid hotelId)
    {
        HotelId = hotelId;
    }
}