using CleanHouse.Domain.Enums;

namespace CleanHouse.Service.DTOs.Payments;

public class PaymentForResultDto
{
    public long Id { get; set; }    
    public long BookingId { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatusType PaymentStatus { get; set; }

    public decimal Amount;
}
