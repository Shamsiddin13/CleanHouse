using AutoMapper;
using CleanHouse.Domain.Entities;
using CleanHouse.Service.DTOs.Bookings;
using CleanHouse.Service.DTOs.Payments;
using CleanHouse.Service.DTOs.Services;
using CleanHouse.Service.DTOs.Users;

namespace CleanHouse.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // User
        CreateMap<User, DTOs.Users.UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        // Booking
        CreateMap<Booking, BookingForCreationDto>().ReverseMap();   
        CreateMap<Booking, BookingForResultDto>().ReverseMap();
        CreateMap<Booking, BookingForUpdateDto>().ReverseMap(); 

        // Service
        CreateMap<Servicing,  ServiceForUpdateDto>().ReverseMap();
        CreateMap<Servicing, DTOs.Services.ServiceForCreationDto>().ReverseMap();    
        CreateMap<Servicing,  ServiceForResultDto>().ReverseMap();   
        
        // Payment
        CreateMap<Payment, PaymentForCreationDto>().ReverseMap(); 
        CreateMap<Payment, PaymentForUpdateDto>().ReverseMap(); 
        CreateMap<Payment, PaymentForResultDto>().ReverseMap(); 
        
    }

}
