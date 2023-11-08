using CleanHouse.Domain.Commons;

namespace CleanHouse.Domain.Entities;

public class Service : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public long ProviderId { get; set; }
    public User User { get; set; }

    public ICollection<Booking> Bookings { get; set; }    
}
