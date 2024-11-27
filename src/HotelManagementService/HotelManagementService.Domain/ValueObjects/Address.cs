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
        if (string.IsNullOrWhiteSpace(street)) throw new DomainException("Street cannot be empty.");
        if (string.IsNullOrWhiteSpace(city)) throw new DomainException("City cannot be empty.");
        if (string.IsNullOrWhiteSpace(country)) throw new DomainException("Country cannot be empty.");

        Street = street;
        City = city;
        Country = country;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {Country}";
    }
}