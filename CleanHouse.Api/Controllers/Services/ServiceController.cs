using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Services;
using CleanHouse.Service.DTOs.Users;
using CleanHouse.Service.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanHouse.Api.Controllers.Services;

public class ServiceController : BaseController
{
    private readonly IServiceCoreService _serviceCoreService;

    public ServiceController(IServiceCoreService serviceCoreService)
    {
        _serviceCoreService = serviceCoreService;
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] ServiceForCreationDto dto)
    => Ok(await _serviceCoreService.AddAsync(dto));


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _serviceCoreService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await _serviceCoreService.RetrieveByIdAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] ServiceForUpdateDto dto)
        => Ok(await _serviceCoreService.ModifyAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await _serviceCoreService.RemoveAsync(id));
}
