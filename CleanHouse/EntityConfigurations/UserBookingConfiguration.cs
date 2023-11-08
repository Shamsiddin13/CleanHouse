using CleanHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanHouse.Data.EntityConfigurations;

public class UserBookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasOne(a => a.User)
        .WithMany(c => c.Bookings)
        .HasForeignKey(a => a.CustomerId);
    }
}
