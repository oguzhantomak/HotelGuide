namespace HotelManagementService.Domain.ValueObjects;

/// <summary>
/// EN: Represents an address value object.
/// TR: Adres value object'ini temsil eder.
/// </summary>
public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new DomainException(ExceptionMessages.StreetCannotBeEmpty);
        if (string.IsNullOrWhiteSpace(city)) throw new DomainException(ExceptionMessages.CityCannotBeEmpty);
        if (string.IsNullOrWhiteSpace(country)) throw new DomainException(ExceptionMessages.CountryCannotBeEmpty);

        Street = street;
        City = city;
        Country = country;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {Country}";
    }
}