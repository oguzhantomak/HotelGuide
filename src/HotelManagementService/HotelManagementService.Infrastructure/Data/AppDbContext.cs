namespace HotelManagementService.Infrastructure.Data;

/// <summary>
/// EN: Database context for the application.
/// TR: Uygulama için veritabanı context'i.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Name).IsRequired().HasMaxLength(200);
            entity.Property(h => h.Address).HasMaxLength(500);
            entity.HasMany(h => h.ContactInformation)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(h => h.ResponsiblePeople)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ContactInformation>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Type).IsRequired();
            entity.Property(c => c.Content).IsRequired();
        });

        modelBuilder.Entity<ResponsiblePerson>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.FirstName).IsRequired();
            entity.Property(r => r.LastName).IsRequired();
        });
    }
}