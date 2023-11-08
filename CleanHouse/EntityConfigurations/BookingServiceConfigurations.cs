using CleanHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanHouse.Data.EntityConfigurations;

public class BookingServiceConfigurations : IEntityTypeConfiguration<Servicing>
{
    public void Configure(EntityTypeBuilder<Servicing> builder)
    {
        builder.HasMany(s => s.Bookings)
        .WithOne(a => a.Service)
        .HasForeignKey(a => a.ServiceId);
    }
}
