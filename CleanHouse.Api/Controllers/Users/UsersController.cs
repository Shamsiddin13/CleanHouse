using CleanHouse.Service.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace CleanHouse.Api.Controllers.Users;

public class UsersController : BaseController
{
    private readonly IUserService _userService;
    //private readonly IConfiguration _configuration;

    public UsersController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        //_configuration = configuration;
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] UserForCreationDto dto)
        => Ok(await _userService.AddAsync(dto));


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _userService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await _userService.RetrieveByIdAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] UserForUpdateDto dto)
        => Ok(await _userService.ModifyAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await _userService.RemoveAsync(id));

    [HttpGet("email")]
    public async Task<IActionResult> GetByEmailAsync(string email)
        => Ok(await _userService.RetrieveByEmailAsync(email));
}
