namespace HotelManagementService.Domain.ValueObjects;

/// <summary>
/// EN: Represents the type of contact information.
/// TR: İletişim bilgisinin türünü temsil eder.
/// </summary>
public class ContactType
{
    public static readonly ContactType Phone = new("Phone");
    public static readonly ContactType Email = new("Email");
    public static readonly ContactType Location = new("Location");

    public string Type { get; private set; }

    private ContactType(string type)
    {
        Type = type;
    }

    /// <summary>
    /// EN: Factory method to create a ContactType from a string.
    /// TR: Bir string değerinden ContactType oluşturmak için fabrika metodu.
    /// </summary>
    public static ContactType FromString(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException(ExceptionMessages.InvalidContactType);

        return type.Trim().ToLowerInvariant() switch
        {
            "phone" => Phone,
            "email" => Email,
            "location" => Location,
            _ => throw new ArgumentException($"{ExceptionMessages.InvalidContactType} {type}")
        };
    }

    public override string ToString()
    {
        return Type;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ContactType other)
        {
            return Type == other.Type;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Type.GetHashCode();
    }
}