using CleanHouse.Domain.Commons;
using CleanHouse.Domain.Enums;

namespace CleanHouse.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public UserRole UserRole { get; set; }
}
