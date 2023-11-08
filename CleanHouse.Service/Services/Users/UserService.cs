﻿using AutoMapper;
using CleanHouse.Data.IRepositories;
using CleanHouse.Domain.Entities;
using CleanHouse.Service.Configurations;
using CleanHouse.Service.DTOs.Users;
using CleanHouse.Service.Exceptions;
using CleanHouse.Service.Extensions;
using CleanHouse.Service.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanHouse.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;

    public UserService(IMapper mapper, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserForResultDto> AddAsync(ServiceForCreationDto dto)
    {
        var users = await _userRepository.SelectAll()
            .Where(u => u.Phone == dto.Phone)
            .FirstOrDefaultAsync();

        if (users is not null)
            throw new CleanHouseException(409, "User is already exist.");

        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;

        var createdUser = await _userRepository.InsertAsync(mappedUser);
        return _mapper.Map<UserForResultDto>(mappedUser);
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CleanHouseException(404, "User not found");

        user.UpdatedAt = DateTime.UtcNow;
        var person = _mapper.Map(dto, user);

        await _userRepository.UpdateAsync(person);

        return _mapper.Map<UserForResultDto>(person);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CleanHouseException(404, "User is not found");

        await _userRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.SelectAll()
            .Include(a => a.Bookings)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> RetrieveByEmailAsync(string email)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Email.ToLower() == email.ToLower())
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CleanHouseException(404, "User Not Found");

        return _mapper.Map<UserForResultDto>(user);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var users = await _userRepository.SelectAll()
            .Where(u => u.Id == id)
            .Include(a => a.Bookings)
            .FirstOrDefaultAsync();
        if (users is null)
            throw new CleanHouseException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(users);
    }

}
