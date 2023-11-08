using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Services;

namespace CleanHouse.Service.Interfaces.Services;

public interface IServiceCoreService
{
    Task<bool> RemoveAsync(long id);
    Task<ServiceForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<ServiceForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<ServiceForResultDto> AddAsync(ServiceForCreationDto dto);
    Task<ServiceForResultDto> ModifyAsync(long id, ServiceForUpdateDto dto);
}
