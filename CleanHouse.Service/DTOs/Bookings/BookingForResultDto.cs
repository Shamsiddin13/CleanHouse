using CleanHouse.Domain.Entities;
using CleanHouse.Domain.Enums;

namespace CleanHouse.Service.DTOs.Bookings;

public class BookingForResultDto
{
    public User User { get; set; }
    public Service Service { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatusType StatusType { get; set; } // Booking status 

    public decimal TotalAmount;
}
