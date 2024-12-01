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
    /// TR: Yeni bir otel oluşturur.</summary>
    /// <param name="request">
    /// EN: Hotel information.
    /// TR: Otel bilgileri.
    /// </param>
    /// <returns>
    /// EN: ID of the created hotel.
    /// TR: Oluşturulan otelin ID'si.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Hotel([FromBody] HotelDto request)
    {
        var hotelId = await _hotelService.CreateHotelAsync(request);
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

    /// <summary>
    /// EN: Adds contact information to a hotel.
    /// TR: Bir otele iletişim bilgisi ekler.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{id}/contact")]
    public async Task<IActionResult> AddContactInformation(Guid id, [FromBody] ContactInformationDto request)
    {
        await _hotelService.AddContactInformationAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// EN: Removes contact information from a hotel.
    /// TR: Bir otelden iletişim bilgisini kaldırır.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="contactId"></param>
    /// <returns></returns>
    [HttpDelete("{id}/contact/{contactId}")]
    public async Task<IActionResult> RemoveContactInformation(Guid id, Guid contactId)
    {
        await _hotelService.RemoveContactInformationAsync(id, contactId);
        return NoContent();
    }

    /// <summary>
    /// EN: Statistics of the hotels.
    /// TR: Otellerin istatistikleri.
    /// </summary>
    /// <returns></returns>
    [HttpGet("getstats")]
    public async Task<IActionResult> GetStatistics()
    {
        var statistics = await _hotelService.GetStatisticsAsync();
        return Ok(statistics);
    }
}