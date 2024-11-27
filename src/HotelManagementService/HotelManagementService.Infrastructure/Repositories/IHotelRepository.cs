namespace HotelManagementService.Infrastructure.Repositories;

/// <summary>
/// EN: Interface for hotel repository operations.
/// TR: Otel repository işlemleri için arayüz.
/// </summary>
public interface IHotelRepository
{
    Task AddAsync(Hotel hotel);
    Task<Hotel> GetByIdAsync(Guid id);
    Task UpdateAsync(Hotel hotel);
    Task DeleteAsync(Guid id);
}