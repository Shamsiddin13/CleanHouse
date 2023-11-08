using CleanHouse.Domain.Enums;

namespace CleanHouse.Service.DTOs.Payments;

public class PaymentForUpdateDto
{
    public long Id { get; set; }    
    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatusType PaymentStatus { get; set; }

    public decimal Amount;
}
