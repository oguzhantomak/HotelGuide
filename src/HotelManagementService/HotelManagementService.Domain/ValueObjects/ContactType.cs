namespace HotelManagementService.Domain.ValueObjects;

/// <summary>
/// EN: Represents the type of contact information.
/// TR: İletişim bilgisinin türünü temsil eder.
/// </summary>
public class ContactType
{
    public static ContactType Phone => new("Phone");
    public static ContactType Email => new("Email");
    public static ContactType Location => new("Location");

    public string Type { get; private set; }

    private ContactType(string type)
    {
        Type = type;
    }

    public override string ToString()
    {
        return Type;
    }
}