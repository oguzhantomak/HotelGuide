using HotelManagementService.Domain.Constants;
using HotelManagementService.Domain.Entities;
using HotelManagementService.Domain.Exceptions;
using HotelManagementService.Domain.ValueObjects;

namespace HotelManagementService.Tests;

public class DomainTests
{
    [Fact]
    public void Hotel_Should_Throw_Exception_When_Name_Is_Empty()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");

        // Act & Assert
        Assert.Throws<DomainException>(() => new Hotel(string.Empty, address));
    }

    [Fact]
    public void Hotel_Should_Create_With_Valid_Parameters()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");

        // Act
        var hotel = new Hotel("Test Hotel", address);

        // Assert
        Assert.NotNull(hotel);
        Assert.Equal("Test Hotel", hotel.Name);
        Assert.Equal(address, hotel.Address);
    }

    [Fact]
    public void AddContactInformation_Should_Throw_Exception_When_ContactInformation_Is_Null()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);

        // Act & Assert
        Assert.Throws<DomainException>(() => hotel.AddContactInformation(null));
    }

    [Fact]
    public void AddContactInformation_Should_Add_ContactInformation()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);
        //var contactInformation = new ContactInformation(new ContactType("Phone"), "123456789");

        var contactInformation = new ContactInformation(ContactType.Phone, "123456789");

        // Act
        hotel.AddContactInformation(contactInformation);

        // Assert
        Assert.Contains(contactInformation, hotel.ContactInformation);
    }

    [Fact]
    public void AddResponsiblePerson_Should_Throw_Exception_When_ResponsiblePerson_Is_Null()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);

        // Act & Assert
        Assert.Throws<DomainException>(() => hotel.AddResponsiblePerson(null));
    }

    [Fact]
    public void AddResponsiblePerson_Should_Add_ResponsiblePerson()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);
        var responsiblePerson = new ResponsiblePerson("Oğuzhan", "Tomak");

        // Act
        hotel.AddResponsiblePerson(responsiblePerson);

        // Assert
        Assert.Contains(responsiblePerson, hotel.ResponsiblePeople);
    }

    [Fact]
    public void UpdateAddress_Should_Throw_Exception_When_Address_Is_Null()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);

        // Act & Assert
        Assert.Throws<DomainException>(() => hotel.UpdateAddress(null));
    }

    [Fact]
    public void UpdateAddress_Should_Update_Address()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);
        var newAddress = new Address("New Street", "New City", "New Country");

        // Act
        hotel.UpdateAddress(newAddress);

        // Assert
        Assert.Equal(newAddress, hotel.Address);
    }

    [Fact]
    public void RemoveContactInformation_Should_Throw_Exception_When_ContactInformation_Not_Found()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);

        // Act & Assert
        Assert.Throws<DomainException>(() => hotel.RemoveContactInformation(Guid.NewGuid()));
    }

    [Fact]
    public void RemoveContactInformation_Should_Remove_ContactInformation()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");
        var hotel = new Hotel("Test Hotel", address);
        var contactInformation = new ContactInformation(ContactType.Phone, "123456789");
        hotel.AddContactInformation(contactInformation);

        // Act
        hotel.RemoveContactInformation(contactInformation.Id);

        // Assert
        Assert.DoesNotContain(contactInformation, hotel.ContactInformation);
    }

    [Fact]
    public void ContactInformation_Should_Throw_Exception_When_Content_Is_Empty()
    {
        // Arrange
        var contactType = ContactType.Phone;

        // Act & Assert
        Assert.Throws<DomainException>(() => new ContactInformation(contactType, string.Empty));
    }

    [Fact]
    public void ContactInformation_Should_Create_With_Valid_Parameters()
    {
        // Arrange
        var contactType = ContactType.Email;
        var content = "test@example.com";

        // Act
        var contactInformation = new ContactInformation(contactType, content);

        // Assert
        Assert.NotNull(contactInformation);
        Assert.Equal(contactType, contactInformation.Type);
        Assert.Equal(content, contactInformation.Content);
    }

    [Fact]
    public void ResponsiblePerson_Should_Throw_Exception_When_FirstName_Is_Empty()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => new ResponsiblePerson(string.Empty, "Tomak"));
    }

    [Fact]
    public void ResponsiblePerson_Should_Throw_Exception_When_LastName_Is_Empty()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => new ResponsiblePerson("Oğuzhan", string.Empty));
    }

    [Fact]
    public void ResponsiblePerson_Should_Create_With_Valid_Parameters()
    {
        // Act
        var responsiblePerson = new ResponsiblePerson("Oğuzhan", "Tomak");

        // Assert
        Assert.NotNull(responsiblePerson);
        Assert.Equal("Oğuzhan", responsiblePerson.FirstName);
        Assert.Equal("Tomak", responsiblePerson.LastName);
    }

    [Fact]
    public void Address_Should_Throw_Exception_When_Street_Is_Empty()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => new Address(string.Empty, "City", "Country"));
    }

    [Fact]
    public void Address_Should_Throw_Exception_When_City_Is_Empty()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => new Address("Street", string.Empty, "Country"));
    }

    [Fact]
    public void Address_Should_Throw_Exception_When_Country_Is_Empty()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => new Address("Street", "City", string.Empty));
    }

    [Fact]
    public void Address_Should_Create_With_Valid_Parameters()
    {
        // Act
        var address = new Address("Street", "City", "Country");

        // Assert
        Assert.NotNull(address);
        Assert.Equal("Street", address.Street);
        Assert.Equal("City", address.City);
        Assert.Equal("Country", address.Country);
    }

    [Fact]
    public void Address_ToString_Should_Return_Correct_Format()
    {
        // Arrange
        var address = new Address("Street", "City", "Country");

        // Act
        var result = address.ToString();

        // Assert
        Assert.Equal("Street, City, Country", result);
    }

    [Fact]
    public void FromString_Should_Return_Correct_ContactType()
    {
        // Act
        var phoneType = ContactType.FromString("Phone");
        var emailType = ContactType.FromString("Email");
        var locationType = ContactType.FromString("Location");

        // Assert
        Assert.Equal(ContactType.Phone, phoneType);
        Assert.Equal(ContactType.Email, emailType);
        Assert.Equal(ContactType.Location, locationType);
    }

    [Fact]
    public void FromString_Should_Throw_Exception_For_Invalid_ContactType()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => ContactType.FromString("InvalidType"));
        Assert.Equal($"{ExceptionMessages.InvalidContactType} InvalidType", exception.Message);
    }

    [Fact]
    public void ToString_Should_Return_Correct_Type()
    {
        // Act
        var phoneType = ContactType.Phone.ToString();
        var emailType = ContactType.Email.ToString();
        var locationType = ContactType.Location.ToString();

        // Assert
        Assert.Equal("Phone", phoneType);
        Assert.Equal("Email", emailType);
        Assert.Equal("Location", locationType);
    }

    [Fact]
    public void Equals_Should_Return_True_For_Same_ContactType()
    {
        // Act
        var phoneType1 = ContactType.Phone;
        var phoneType2 = ContactType.FromString("Phone");

        // Assert
        Assert.True(phoneType1.Equals(phoneType2));
    }

    [Fact]
    public void Equals_Should_Return_False_For_Different_ContactType()
    {
        // Act
        var phoneType = ContactType.Phone;
        var emailType = ContactType.Email;

        // Assert
        Assert.False(phoneType.Equals(emailType));
    }

    [Fact]
    public void GetHashCode_Should_Return_Same_HashCode_For_Same_ContactType()
    {
        // Act
        var phoneType1 = ContactType.Phone;
        var phoneType2 = ContactType.FromString("Phone");

        // Assert
        Assert.Equal(phoneType1.GetHashCode(), phoneType2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_Should_Return_Different_HashCode_For_Different_ContactType()
    {
        // Act
        var phoneType = ContactType.Phone;
        var emailType = ContactType.Email;

        // Assert
        Assert.NotEqual(phoneType.GetHashCode(), emailType.GetHashCode());
    }
}