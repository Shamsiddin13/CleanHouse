using CleanHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanHouse.Data.EntityConfigurations;

public class BookingUserConfigurations : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(c => c.Bookings)
        .WithOne(a => a.User)
        .HasForeignKey(a => a.CustomerId);
    }
}
