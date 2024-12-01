using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportService.API.Controllers;
using ReportService.Application.Dtos;
using ReportService.Application.Interfaces;

namespace ReportService.Tests;

public class DomainTests
{
    private readonly Mock<IReportService> _mockReportService;
    private readonly ReportController _controller;

    public DomainTests()
    {
        _mockReportService = new Mock<IReportService>();
        _controller = new ReportController(_mockReportService.Object);
    }


    [Fact]
    public async Task GetReportDetails_ReturnsOkResult_WithReport()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var report = new ReportDto { Id = reportId };
        _mockReportService.Setup(service => service.GetReportDetailsAsync(reportId)).ReturnsAsync(report);

        // Act
        var result = await _controller.GetReportDetails(reportId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ReportDto>(okResult.Value);
        Assert.Equal(reportId, returnValue.Id);
    }

    [Fact]
    public async Task GetReportDetails_ReturnsNotFound_WhenReportIsNull()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        _mockReportService.Setup(service => service.GetReportDetailsAsync(reportId)).ReturnsAsync((ReportDto)null);

        // Act
        var result = await _controller.GetReportDetails(reportId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAllReports_ReturnsOkResult_WithListOfReports()
    {
        // Arrange
        var reports = new List<ReportDto> { new ReportDto { Id = Guid.NewGuid() } };
        _mockReportService.Setup(service => service.GetAllReportsAsync()).ReturnsAsync(reports);

        // Act
        var result = await _controller.GetAllReports();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ReportDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
}