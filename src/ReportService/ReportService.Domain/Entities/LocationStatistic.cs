using ReportService.Domain.Constants;

namespace ReportService.Domain.Entities;

/// <summary>
/// EN: Represents location-based statistics.
/// TR: Konuma dayalı istatistikleri temsil eder.
/// </summary>
public class LocationStatistic
{
    public string Location { get; private set; }
    public int HotelCount { get; private set; }
    public int PhoneNumberCount { get; private set; }

    public LocationStatistic(string location, int hotelCount, int phoneNumberCount)
    {
        if (string.IsNullOrWhiteSpace(location)) throw new DomainException(ExceptionMessages.LocationCannotBeEmpty);
        Location = location;
        HotelCount = hotelCount;
        PhoneNumberCount = phoneNumberCount;
    }
}