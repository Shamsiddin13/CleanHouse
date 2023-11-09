using CleanHouse.Service.Interfaces.Booking;
using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Bookings;
using CleanHouse.Data.IRepositories;
using CleanHouse.Service.Exceptions;
using CleanHouse.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using CleanHouse.Domain.Entities;
using AutoMapper;

namespace CleanHouse.Service.Services.Bookings;

public class BookingService : IBookingService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Booking> _repositoryBooking;

    public BookingService(IMapper mapper, IRepository<Booking> repositoryBooking = null)
    {
        _mapper = mapper;
        _repositoryBooking = repositoryBooking;
    }


    public async Task<BookingForResultDto> AddAsync(BookingForCreationDto dto)
    {
        var bookings = await _repositoryBooking.SelectAll()
            .Where(u => u.CustomerId == dto.CustomerId)
            .FirstOrDefaultAsync();

        if (bookings is not null)
            throw new CleanHouseException(409, "Book is already exist.");

        var mappedBooking = _mapper.Map<Booking>(dto);
        mappedBooking.CreatedAt = DateTime.UtcNow;
        mappedBooking.StartTime = DateTime.UtcNow;

        var createdBooking = await _repositoryBooking.InsertAsync(mappedBooking);
        return _mapper.Map<BookingForResultDto>(mappedBooking);
    }

    public async Task<BookingForResultDto> ModifyAsync(long id, BookingForUpdateDto dto)
    {
        var booking = await _repositoryBooking.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (booking is null)
            throw new CleanHouseException(404, "Book not found");

        booking.UpdatedAt = DateTime.UtcNow;
        var book = _mapper.Map(dto, booking);

        await _repositoryBooking.UpdateAsync(book);

        return _mapper.Map<BookingForResultDto>(book);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var book = await _repositoryBooking.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (book is null)
            throw new CleanHouseException(404, "Book is not found");

        await _repositoryBooking.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<BookingForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var bookings = await _repositoryBooking.SelectAll()
                .Include(a => a.Service)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

        return _mapper.Map<IEnumerable<BookingForResultDto>>(bookings);
    }

    public async Task<BookingForResultDto> RetrieveByIdAsync(long id)
    {
        var bookings = await _repositoryBooking.SelectAll()
                .Where(u => u.Id == id)
                .Include(a => a.Service)
                .FirstOrDefaultAsync();
        if (bookings is null)
            throw new CleanHouseException(404, "Book is not found");

        return _mapper.Map<BookingForResultDto>(bookings);
    }
}
