using FluentValidation.TestHelper;
using HotelManagementService.Application.Constants;
using HotelManagementService.Application.Dtos;
using HotelManagementService.Application.Services;
using HotelManagementService.Application.Validators;
using HotelManagementService.Domain.Entities;
using HotelManagementService.Domain.ValueObjects;
using HotelManagementService.Infrastructure.Repositories;
using Moq;

namespace HotelManagementService.Tests;

public class ApplicationTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly HotelService _hotelService;
    private readonly HotelValidator _validator;


    public ApplicationTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();
        _hotelService = new HotelService(_hotelRepositoryMock.Object);
        _validator = new HotelValidator();

    }

    [Fact]
    public async Task CreateHotelAsync_ShouldReturnHotelId()
    {
        // Arrange
        var hotelDto = new HotelDto
        {
            Name = "Test Hotel",
            Street = "Test Street",
            City = "Test City",
            Country = "Test Country",
            ContactInformation = new List<ContactInformationDto>
            {
                new ContactInformationDto { Type = "Phone", Content = "123456789" }
            },
            ResponsiblePeople = new List<ResponsiblePersonDto>
            {
                new ResponsiblePersonDto { FirstName = "Oğuzhan", LastName = "Tomak" }
            }
        };

        var hotel = new Hotel(hotelDto.Name, new Address(hotelDto.Street, hotelDto.City, hotelDto.Country));
        _hotelRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Hotel>())).Returns(Task.CompletedTask);

        // Act
        var result = await _hotelService.CreateHotelAsync(hotelDto);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        _hotelRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Hotel>()), Times.Once);
    }

    [Fact]
    public async Task AddContactInformationAsync_ShouldAddContactInformation()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var contactInformationDto = new ContactInformationDto { Type = "Email", Content = "test@example.com" };
        var hotel = new Hotel("Test Hotel", new Address("Test Street", "Test City", "Test Country"));

        _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);
        _hotelRepositoryMock.Setup(repo => repo.UpdateAsync(hotel)).Returns(Task.CompletedTask);

        // Act
        await _hotelService.AddContactInformationAsync(hotelId, contactInformationDto);

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.GetByIdAsync(hotelId), Times.Once);
        _hotelRepositoryMock.Verify(repo => repo.UpdateAsync(hotel), Times.Once);
    }

    [Fact]
    public async Task AddResponsiblePersonAsync_ShouldAddResponsiblePerson()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var responsiblePersonDto = new ResponsiblePersonDto { FirstName = "Jane", LastName = "Tomak" };
        var hotel = new Hotel("Test Hotel", new Address("Test Street", "Test City", "Test Country"));

        _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);
        _hotelRepositoryMock.Setup(repo => repo.UpdateAsync(hotel)).Returns(Task.CompletedTask);

        // Act
        await _hotelService.AddResponsiblePersonAsync(hotelId, responsiblePersonDto);

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.GetByIdAsync(hotelId), Times.Once);
        _hotelRepositoryMock.Verify(repo => repo.UpdateAsync(hotel), Times.Once);
    }

    [Fact]
    public async Task UpdateHotelAddressAsync_ShouldUpdateAddress()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var hotel = new Hotel("Test Hotel", new Address("Old Street", "Old City", "Old Country"));

        _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);
        _hotelRepositoryMock.Setup(repo => repo.UpdateAsync(hotel)).Returns(Task.CompletedTask);

        // Act
        await _hotelService.UpdateHotelAddressAsync(hotelId, "New Street", "New City", "New Country");

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.GetByIdAsync(hotelId), Times.Once);
        _hotelRepositoryMock.Verify(repo => repo.UpdateAsync(hotel), Times.Once);
    }

    [Fact]
    public async Task GetHotelDetailsAsync_ShouldReturnHotelDetails()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var hotel = new Hotel("Test Hotel", new Address("Test Street", "Test City", "Test Country"));

        _hotelRepositoryMock.Setup(repo => repo.GetByIdAsync(hotelId)).ReturnsAsync(hotel);

        // Act
        var result = await _hotelService.GetHotelDetailsAsync(hotelId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hotel.Name, result.Name);
        _hotelRepositoryMock.Verify(repo => repo.GetByIdAsync(hotelId), Times.Once);
    }

    [Fact]
    public async Task DeleteHotelAsync_ShouldDeleteHotel()
    {
        // Arrange
        var hotelId = Guid.NewGuid();

        _hotelRepositoryMock.Setup(repo => repo.DeleteAsync(hotelId)).Returns(Task.CompletedTask);

        // Act
        await _hotelService.DeleteHotelAsync(hotelId);

        // Assert
        _hotelRepositoryMock.Verify(repo => repo.DeleteAsync(hotelId), Times.Once);
    }

    [Fact]
    public async Task GetStatisticsAsync_ShouldReturnStatistics()
    {
        // Arrange
        var hotels = new List<Hotel>
        {
            new Hotel("Hotel 1", new Address("Street 1", "City 1", "Country 1")),
            new Hotel("Hotel 2", new Address("Street 2", "City 2", "Country 2"))
        };

        _hotelRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(hotels);

        // Act
        var result = await _hotelService.GetStatisticsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _hotelRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }


    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new HotelDto { Name = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(h => h.Name).WithErrorMessage(ValidationMessages.HotelNameRequired);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Specified()
    {
        var model = new HotelDto { Name = "Test Hotel" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(h => h.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Street_Is_Empty()
    {
        var model = new HotelDto { Street = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(h => h.Street).WithErrorMessage(ValidationMessages.StreetRequired);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Street_Is_Specified()
    {
        var model = new HotelDto { Street = "Test Street" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(h => h.Street);
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_Empty()
    {
        var model = new HotelDto { City = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(h => h.City).WithErrorMessage(ValidationMessages.CityRequired);
    }

    [Fact]
    public void Should_Not_Have_Error_When_City_Is_Specified()
    {
        var model = new HotelDto { City = "Test City" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(h => h.City);
    }

    [Fact]
    public void Should_Have_Error_When_Country_Is_Empty()
    {
        var model = new HotelDto { Country = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(h => h.Country).WithErrorMessage(ValidationMessages.CountryRequired);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Country_Is_Specified()
    {
        var model = new HotelDto { Country = "Test Country" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(h => h.Country);
    }
}