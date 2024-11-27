﻿using Microsoft.AspNetCore.Mvc;

namespace HotelManagementService.API.Controllers;


/// <summary>
/// EN: Manages hotel-related operations.
/// TR: Otel ile ilgili işlemleri yönetir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// EN: Creates a new hotel.
    /// TR: Yeni bir otel oluşturur.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Hotel(string name, string address)
    {
        //TODO:parametre CreateHotelRequest'e çevirilecek!

        var hotelId = await _hotelService.CreateHotelAsync(name, address);
        return CreatedAtAction(nameof(Hotel), new { id = hotelId }, null);
    }

    /// <summary>
    /// EN: Retrieves hotel details by ID.
    /// TR: ID'ye göre otel bilgilerini getirir.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> Hotel(Guid id)
    {
        var hotel = await _hotelService.GetHotelDetailsAsync(id);
        if (hotel == null) return NotFound();
        return Ok(hotel);
    }

    /// <summary>
    /// EN: Deletes a hotel by ID.
    /// TR: ID'ye göre bir oteli siler.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> HotelDelete(Guid id)
    {
        await _hotelService.DeleteHotelAsync(id);
        return NoContent();
    }
}