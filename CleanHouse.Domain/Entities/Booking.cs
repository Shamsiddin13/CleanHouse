using CleanHouse.Domain.Commons;
using CleanHouse.Domain.Enums;

namespace CleanHouse.Domain.Entities;

public class Booking : Auditable
{
    public long CustomerId { get; set; } // Foreign key to User (Customer)
    public User User { get; set; } 

    public long ServiceId { get; set; } // Foreign key to Service
    public Service Service { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public BookingStatusType StatusType { get; set; } // Booking status 
    
    private decimal _totalAmount; // Private field to store the calculated total amount
    public decimal TotalAmount
    {
        get
        {
            // Calculate the total amount based on the service's price and duration
            TimeSpan duration = EndTime - StartTime;
            decimal servicePrice = Service.Price;
            _totalAmount = servicePrice * (decimal)duration.TotalHours;
            return _totalAmount;
        }
        set
        {
            _totalAmount = value;
        }
    }
}
