using CleanHouse.Domain.Commons;
using CleanHouse.Domain.Enums;

namespace CleanHouse.Domain.Entities;

public class Payment : Auditable
{

    public long BookingId { get; set; } // Foreign key to Booking
    public Booking Booking { get; set; } 

    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }    
    public PaymentStatusType PaymentStatus { get; set; }    

    private decimal _amount;
    public decimal Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            // Add validation logic to ensure the amount is non-negative
            if (value < 0)
            {
                throw new ArgumentException("Amount cannot be negative.");
            }
            _amount = value;
        }
    }
}
