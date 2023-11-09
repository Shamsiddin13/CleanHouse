using CleanHouse.Service.Interfaces.Booking;
using Microsoft.AspNetCore.Authorization;
using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace CleanHouse.Api.Controllers.Bookings;

public class BookingController : BaseController
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] BookingForCreationDto dto)
    => Ok(await _bookingService.AddAsync(dto));


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _bookingService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await _bookingService.RetrieveByIdAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BookingForUpdateDto dto)
        => Ok(await _bookingService.ModifyAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await _bookingService.RemoveAsync(id));
}
