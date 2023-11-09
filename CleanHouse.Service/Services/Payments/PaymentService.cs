using AutoMapper;
using CleanHouse.Data.IRepositories;
using CleanHouse.Domain.Entities;
using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Payments;
using CleanHouse.Service.DTOs.Users;
using CleanHouse.Service.Exceptions;
using CleanHouse.Service.Extensions;
using CleanHouse.Service.Interfaces.Payment;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CleanHouse.Service.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Payment> _paymentRepository;

    public PaymentService(IMapper mapper, IRepository<Payment> paymentRepository)
    {
        _mapper = mapper;
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto)
    {
        var payments = await _paymentRepository.SelectAll()
            .Where(u => u.BookingId == dto.BookingId)
            .FirstOrDefaultAsync();

        if (payments is not null)
            throw new CleanHouseException(409, "Payment is already exist.");

        var mappedPayment = _mapper.Map<Payment>(dto);
        mappedPayment.CreatedAt = DateTime.UtcNow;
        mappedPayment.PaymentDate = DateTime.UtcNow;


        var createdPayment = await _paymentRepository.InsertAsync(mappedPayment);
        return _mapper.Map<PaymentForResultDto>(mappedPayment);
    }

    public async Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto)
    {
        var payment = await _paymentRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (payment is null)
            throw new CleanHouseException(404, "Payment not found");

        payment.UpdatedAt = DateTime.UtcNow;
        var pay = _mapper.Map(dto, payment);

        await _paymentRepository.UpdateAsync(pay);

        return _mapper.Map<PaymentForResultDto>(pay);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var pay = await _paymentRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (pay is null)
            throw new CleanHouseException(404, "Payment is not found");

        await _paymentRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var payments = await _paymentRepository.SelectAll()
            .Include(a => a.Booking)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PaymentForResultDto>>(payments);
    }

    public async Task<PaymentForResultDto> RetrieveByIdAsync(long id)
    {
        var payments = await _paymentRepository.SelectAll()
            .Where(u => u.Id == id)
            .Include(a => a.Booking)
            .FirstOrDefaultAsync();
        if (payments is null)
            throw new CleanHouseException(404, "Payment is not found");

        return _mapper.Map<PaymentForResultDto>(payments);
    }
}
