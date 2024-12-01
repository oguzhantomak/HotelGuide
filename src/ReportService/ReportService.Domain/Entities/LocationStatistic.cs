using MongoDB.Bson.Serialization.Attributes;
using ReportService.Domain.Constants;

namespace ReportService.Domain.Entities;

/// <summary>
/// EN: Represents location-based statistics.
/// TR: Konuma dayalı istatistikleri temsil eder.
/// </summary>
public class LocationStatistic
{
    [BsonElement("location")]
    public string Location { get; private set; }

    [BsonElement("hotelCount")]
    public int HotelCount { get; private set; }

    [BsonElement("contactInformationCount")]
    public int ContactInformationCount { get; private set; }

    [BsonElement("responsiblePersonCount")]
    public int ResponsiblePersonCount { get; private set; }

    public LocationStatistic(string location, int hotelCount, int contactInformationCount, int responsiblePersonCount)
    {
        if (string.IsNullOrWhiteSpace(location)) throw new DomainException(ExceptionMessages.LocationCannotBeEmpty);
        Location = location;
        HotelCount = hotelCount;
        ContactInformationCount = contactInformationCount;
        ResponsiblePersonCount = responsiblePersonCount;
    }
}