using CleanHouse.Domain.Entities;

namespace CleanHouse.Service.DTOs.Services;

public class ServiceForResultDto
{
    public long Id { get; set; }    
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public User Provider { get; set; }
}
