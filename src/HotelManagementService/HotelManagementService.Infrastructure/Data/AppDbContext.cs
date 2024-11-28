namespace HotelManagementService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<ContactInformation> ContactInformations { get; set; }
    public DbSet<ResponsiblePerson> ResponsiblePeople { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<HotelCreatedEvent>();
        ConfigureHotelEntity(modelBuilder.Entity<Hotel>());
        ConfigureContactInformationEntity(modelBuilder.Entity<ContactInformation>());
        ConfigureResponsiblePersonEntity(modelBuilder.Entity<ResponsiblePerson>());
    }

    private void ConfigureHotelEntity(EntityTypeBuilder<Hotel> entity)
    {
        entity.HasKey(h => h.Id);

        entity.Property(h => h.Name)
              .IsRequired()
              .HasMaxLength(200);

        // Address Configuration as Owned Type
        entity.OwnsOne(h => h.Address, address =>
        {
            address.Property(a => a.Street)
                   .HasMaxLength(200)
                   .IsRequired();
            address.Property(a => a.City)
                   .HasMaxLength(100)
                   .IsRequired();
            address.Property(a => a.Country)
                   .HasMaxLength(100)
                   .IsRequired();
        });

        entity.HasMany(h => h.ContactInformation)
              .WithOne()
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(h => h.ResponsiblePeople)
              .WithOne()
              .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureContactInformationEntity(EntityTypeBuilder<ContactInformation> entity)
    {
        entity.HasKey(c => c.Id);

        var contactTypeConverter = new ValueConverter<ContactType, string>(
            v => v.ToString(),
            v => ContactType.FromString(v)
        );

        entity.Property(c => c.Type)
              .HasConversion(contactTypeConverter)
              .IsRequired()
              .HasMaxLength(50);

        entity.Property(c => c.Content)
              .IsRequired()
              .HasMaxLength(200);
    }

    private void ConfigureResponsiblePersonEntity(EntityTypeBuilder<ResponsiblePerson> entity)
    {
        entity.HasKey(r => r.Id);

        entity.Property(r => r.FirstName)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(r => r.LastName)
              .IsRequired()
              .HasMaxLength(100);
    }
}
