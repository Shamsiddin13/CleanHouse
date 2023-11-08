using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Payments;

namespace CleanHouse.Service.Interfaces.Payment;

public interface IPaymentService
{
    Task<bool> RemoveAsync(long id);
    Task<PaymentForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto);
    Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto);
}
