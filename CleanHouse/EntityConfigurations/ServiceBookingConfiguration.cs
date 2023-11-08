using CleanHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CleanHouse.Data.EntityConfigurations;

public class ServiceBookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasOne(a => a.Service)
        .WithMany(s => s.Bookings)
        .HasForeignKey(a => a.ServiceId);
    }
}
