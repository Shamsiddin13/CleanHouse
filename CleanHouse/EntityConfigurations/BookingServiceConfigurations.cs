using CleanHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanHouse.Data.EntityConfigurations;

public class BookingServiceConfigurations : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasMany(s => s.Bookings)
        .WithOne(a => a.Service)
        .HasForeignKey(a => a.ServiceId);
    }
}
