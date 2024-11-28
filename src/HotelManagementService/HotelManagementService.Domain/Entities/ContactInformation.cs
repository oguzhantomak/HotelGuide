namespace HotelManagementService.Domain.Entities;

/// <summary>
/// EN: Represents contact information for a hotel.
/// TR: Bir otelin iletişim bilgilerini temsil eder.
/// </summary>
public class ContactInformation
{
    public Guid Id { get; private set; }
    public ContactType Type { get; private set; }
    public string Content { get; private set; }

    public ContactInformation(ContactType type, string content)
    {
        if (string.IsNullOrWhiteSpace(content)) throw new DomainException(ExceptionMessages.ContentCannotBeEmpty);
        Id = Guid.NewGuid();
        Type = type;
        Content = content;
    }
}