namespace CleanHouse.Service.DTOs.Services;

public class ServiceForCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public long ProviderId { get; set; }
}
