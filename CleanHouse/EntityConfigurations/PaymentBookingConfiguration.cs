using CleanHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanHouse.Data.EntityConfigurations;

public class PaymentBookingConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        //builder.HasOne(p => p.Booking)
        //.WithOne(b => b.Payment)
        //.HasForeignKey<Payment>(p => p.BookingId);
    }
}
