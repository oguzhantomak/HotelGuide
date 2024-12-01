using HotelManagementService.API.Controllers;
using HotelManagementService.Application.Dtos;
using HotelManagementService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelManagementService.Tests;

public class HotelControllerTests
{
    private readonly Mock<IHotelService> _mockHotelService;
    private readonly HotelController _controller;

    public HotelControllerTests()
    {
        _mockHotelService = new Mock<IHotelService>();
        _controller = new HotelController(_mockHotelService.Object);
    }

    [Fact]
    public async Task Hotel_Post_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var hotelDto = new HotelDto();
        var hotelId = Guid.NewGuid();
        _mockHotelService.Setup(service => service.CreateHotelAsync(hotelDto)).ReturnsAsync(hotelId);

        // Act
        var result = await _controller.Hotel(hotelDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.Hotel), createdAtActionResult.ActionName);
        Assert.Equal(hotelId, createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Hotel_Get_ReturnsOkObjectResult_WhenHotelExists()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var hotel = new HotelDto();
        _mockHotelService.Setup(service => service.GetHotelDetailsAsync(hotelId)).ReturnsAsync(hotel);

        // Act
        var result = await _controller.Hotel(hotelId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(hotel, okResult.Value);
    }

    [Fact]
    public async Task Hotel_Get_ReturnsNotFoundResult_WhenHotelDoesNotExist()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        _mockHotelService.Setup(service => service.GetHotelDetailsAsync(hotelId)).ReturnsAsync((HotelDto)null);

        // Act
        var result = await _controller.Hotel(hotelId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task HotelDelete_Delete_ReturnsNoContentResult()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        _mockHotelService.Setup(service => service.DeleteHotelAsync(hotelId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.HotelDelete(hotelId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task AddContactInformation_Post_ReturnsNoContentResult()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var contactInformationDto = new ContactInformationDto();
        _mockHotelService.Setup(service => service.AddContactInformationAsync(hotelId, contactInformationDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.AddContactInformation(hotelId, contactInformationDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveContactInformation_Delete_ReturnsNoContentResult()
    {
        // Arrange
        var hotelId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        _mockHotelService.Setup(service => service.RemoveContactInformationAsync(hotelId, contactId)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RemoveContactInformation(hotelId, contactId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}