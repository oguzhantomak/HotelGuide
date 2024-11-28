namespace HotelManagementService.Domain.Entities;

/// <summary>
/// EN: Represents the aggregate root for Hotel.
/// TR: Otel için aggregate root'u temsil eder.
/// </summary>
public class Hotel : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    private readonly List<ContactInformation> _contactInformation = new();
    public IReadOnlyCollection<ContactInformation> ContactInformation => _contactInformation.AsReadOnly();
    private readonly List<ResponsiblePerson> _responsiblePeople = new();
    public IReadOnlyCollection<ResponsiblePerson> ResponsiblePeople => _responsiblePeople.AsReadOnly();

    // Domain Events
    private readonly List<HotelCreatedEvent> _domainEvents = new();
    public IReadOnlyCollection<HotelCreatedEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Hotel(string name, Address address)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Hotel name cannot be empty.");
        Id = Guid.NewGuid();
        Name = name;
        Address = address;

        // Trigger domain event
        _domainEvents.Add(new HotelCreatedEvent(Id));
    }

    public void AddContactInformation(ContactInformation contactInformation)
    {
        if (contactInformation == null) throw new DomainException("Contact information cannot be null.");
        _contactInformation.Add(contactInformation);
    }

    public void AddResponsiblePerson(ResponsiblePerson responsiblePerson)
    {
        if (responsiblePerson == null) throw new DomainException("Responsible person cannot be null.");
        _responsiblePeople.Add(responsiblePerson);
    }

    public void UpdateAddress(Address newAddress)
    {
        if (newAddress == null) throw new DomainException("Address cannot be null.");
        Address = newAddress;
    }

    public void RemoveContactInformation(Guid contactId)
    {
        var contact = _contactInformation.FirstOrDefault(x => x.Id == contactId);
        if (contact == null) throw new DomainException("Contact information not found.");
        _contactInformation.Remove(contact);
    }
}