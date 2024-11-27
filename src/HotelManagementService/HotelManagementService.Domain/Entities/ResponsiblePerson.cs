namespace HotelManagementService.Domain.Entities;

/// <summary>
/// EN: Represents a responsible person for a hotel.
/// TR: Bir otel için yetkili kişiyi temsil eder.
/// </summary>
public class ResponsiblePerson
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public ResponsiblePerson(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainException("First name cannot be empty.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainException("Last name cannot be empty.");
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
}