using CleanHouse.Service.Configurations;
using CleanHouse.Service.Interfaces.Payment;
using Microsoft.AspNetCore.Authorization;
using CleanHouse.Service.DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace CleanHouse.Api.Controllers.Payments;

public class PaymentController : BaseController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] PaymentForCreationDto dto)
    => Ok(await _paymentService.AddAsync(dto));


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await _paymentService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await _paymentService.RetrieveByIdAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] PaymentForUpdateDto dto)
        => Ok(await _paymentService.ModifyAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await _paymentService.RemoveAsync(id));
}
