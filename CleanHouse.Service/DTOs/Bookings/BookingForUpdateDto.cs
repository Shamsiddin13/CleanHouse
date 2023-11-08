using CleanHouse.Domain.Enums;

namespace CleanHouse.Service.DTOs.Bookings;

public class BookingForUpdateDto
{
    public long CustomerId { get; set; }
    public long ServiceId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatusType StatusType { get; set; }

    public decimal TotalAmount;
}
