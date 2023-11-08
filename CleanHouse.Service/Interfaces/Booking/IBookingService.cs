using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Bookings;

namespace CleanHouse.Service.Interfaces.Booking;

public interface IBookingService
{
    Task<bool> RemoveAsync(long id);
    Task<BookingForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<BookingForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<BookingForResultDto> AddAsync(BookingForCreationDto dto);
    Task<BookingForResultDto> ModifyAsync(long id, BookingForUpdateDto dto);
}
