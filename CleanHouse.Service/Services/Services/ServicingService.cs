using AutoMapper;
using CleanHouse.Data.IRepositories;
using CleanHouse.Domain.Entities;
using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Services;
using CleanHouse.Service.DTOs.Users;
using CleanHouse.Service.Exceptions;
using CleanHouse.Service.Extensions;
using CleanHouse.Service.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace CleanHouse.Service.Services.Services;

public class ServicingService : IServiceCoreService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Servicing> _serviceRepository;

    public ServicingService(IMapper mapper, IRepository<Servicing> serviceRepository)
    {
        _mapper = mapper;
        _serviceRepository = serviceRepository;
    }

    public async Task<ServiceForResultDto> AddAsync(DTOs.Services.ServiceForCreationDto dto)
    {
        var services = await _serviceRepository.SelectAll()
            .Where(u => u.ProviderId == dto.ProviderId)
            .FirstOrDefaultAsync();

        if (services is not null)
            throw new CleanHouseException(409, "Service is already exist.");

        var mappedService = _mapper.Map<Servicing>(dto);
        mappedService.CreatedAt = DateTime.UtcNow;

        var createdService = await _serviceRepository.InsertAsync(mappedService);
        return _mapper.Map<ServiceForResultDto>(mappedService);
    }

    public async Task<ServiceForResultDto> ModifyAsync(long id, ServiceForUpdateDto dto)
    {
        var service = await _serviceRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (service is null)
            throw new CleanHouseException(404, "Service not found");

        service.UpdatedAt = DateTime.UtcNow;
        var serv = _mapper.Map(dto, service);

        await _serviceRepository.UpdateAsync(serv);

        return _mapper.Map<ServiceForResultDto>(serv);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var service = await _serviceRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (service is null)
            throw new CleanHouseException(404, "Service is not found");

        await _serviceRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<ServiceForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var services = await _serviceRepository.SelectAll()
            .Include(a => a.Bookings)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ServiceForResultDto>>(services);
    }

    public async Task<ServiceForResultDto> RetrieveByIdAsync(long id)
    {
        var services = await _serviceRepository.SelectAll()
            .Where(u => u.Id == id)
            .Include(a => a.Bookings)
            .FirstOrDefaultAsync();
        if (services is null)
            throw new CleanHouseException(404, "Service is not found");

        return _mapper.Map<ServiceForResultDto>(services);
    }

}
