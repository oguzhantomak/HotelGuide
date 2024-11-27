using HotelManagementService.Infrastructure.Data;

namespace HotelManagementService.Infrastructure.Repositories;

/// <summary>
/// EN: EF Core implementation for hotel repository.
/// TR: Otel repository için EF Core implementasyonu.
/// </summary>
public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task<Hotel> GetByIdAsync(Guid id)
    {
        return await _context.Hotels
            .Include(h => h.ContactInformation)
            .Include(h => h.ResponsiblePeople)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }
}