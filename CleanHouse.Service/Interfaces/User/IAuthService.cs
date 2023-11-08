using CleanHouse.Service.DTOs.Users;

namespace CleanHouse.Service.Interfaces.User;

public interface IAuthService
{
    public Task<LoginResultDto> AuthenticateAsync(LoginDto loginDto);    
}
