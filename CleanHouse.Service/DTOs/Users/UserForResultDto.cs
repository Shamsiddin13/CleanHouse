using CleanHouse.Domain.Entities;

namespace CleanHouse.Service.DTOs.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    //public ICollection<BookingForResultDto> Bookings { get; set; }
}
