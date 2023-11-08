using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Users;

namespace CleanHouse.Service.Interfaces.User;

public interface IUserService
{
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<UserForResultDto> RetrieveByEmailAsync(string email);
    Task<UserForResultDto> AddAsync(ServiceForCreationDto dto);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<bool> RemoveAsync(long id);
}
