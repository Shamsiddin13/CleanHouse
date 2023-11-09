using Microsoft.AspNetCore.Authentication.JwtBearer;
using CleanHouse.Service.Interfaces.Services;
using CleanHouse.Service.Interfaces.Booking;
using CleanHouse.Service.Interfaces.Commons;
using CleanHouse.Service.Interfaces.Payment;
using CleanHouse.Service.Services.Bookings;
using CleanHouse.Service.Services.Payments;
using CleanHouse.Service.Services.Services;
using CleanHouse.Service.Services.Commons;
using CleanHouse.Service.Interfaces.User;
using CleanHouse.Service.Services.Users;
using Microsoft.IdentityModel.Tokens;
using CleanHouse.Data.IRepositories;
using CleanHouse.Data.Repositories;
using CleanHouse.Service.Helpers;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace CleanHouse.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceCoreService, ServiceCoreService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();


            services.AddScoped<IFileService, FileService>();
            services.AddScoped<WebHostEnviromentHelper, WebHostEnviromentHelper>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanHouse.Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{ }
            }
        });
            });
        }
    }
}
