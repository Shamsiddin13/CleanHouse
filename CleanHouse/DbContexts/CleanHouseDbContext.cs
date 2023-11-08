using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanHouse.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using CleanHouse.Domain.Entities;

namespace CleanHouse.Data.DbContexts;

public class CleanHouseDbContext : DbContext
{
    public CleanHouseDbContext(DbContextOptions<CleanHouseDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingServiceConfigurations());

        modelBuilder.ApplyConfiguration(new BookingUserConfigurations());

        modelBuilder.ApplyConfiguration(new PaymentBookingConfiguration());

        modelBuilder.ApplyConfiguration(new ServiceBookingConfiguration());

        modelBuilder.ApplyConfiguration(new UserBookingConfiguration());

        Task.Run(() =>
        {
            SeedUsers(modelBuilder);
        }).Wait();
    }

    private void SeedUsers(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasData(new User[]
            {
                new User
                {
                    Id = 1,
                    FirstName = "Shamsiddin",
                    LastName = "Umarov",
                    Email = "shamsiddin@gmail.com",
                    Phone = "+998979789559",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    FirstName = "Normadjon",
                    LastName = "G'offorov",
                    Email = "normadjon@gmail.com",
                    Phone = "+998914995906",
                    CreatedAt = DateTime.UtcNow
                }
            });
    }


    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.ToTable(nameof(Users));
        // Configure the User entity
        modelBuilder.HasKey(u => u.Id);
        modelBuilder.Property(u => u.FirstName).IsRequired();
        modelBuilder.Property(u => u.LastName).IsRequired();
        modelBuilder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        modelBuilder.HasIndex(u => u.Email).IsUnique();
        modelBuilder.Property(u => u.Password).IsRequired();
    }

    public void Configure(EntityTypeBuilder<Service> modelBuilder)
    {
        modelBuilder.ToTable(nameof(Services));
        modelBuilder.HasKey(s => s.Id);
        modelBuilder.Property(s => s.Name).IsRequired();
        modelBuilder.Property(s => s.Description).IsRequired();
        modelBuilder.Property(s => s.Price).IsRequired();
    }

    public void Configure(EntityTypeBuilder<Booking> modelBuilder)
    {
        modelBuilder.ToTable(nameof(Bookings));
        modelBuilder.HasKey(b => b.Id);
        modelBuilder.Property(b => b.StartTime).IsRequired();
        modelBuilder.Property(b => b.EndTime).IsRequired();
        modelBuilder.Property(b => b.TotalAmount).IsRequired();
    }

    public void Configure(EntityTypeBuilder<Payment> modelBuilder)
    {
        modelBuilder.HasKey(p => p.Id);
        modelBuilder.Property(p => p.Amount).IsRequired();
        modelBuilder.Property(p => p.PaymentDate).IsRequired();
        modelBuilder.Property(p => p.PaymentStatus).IsRequired();
        modelBuilder.Property(p => p.Amount)
        .IsRequired()
        .HasPrecision(10, 2);
    }

}
