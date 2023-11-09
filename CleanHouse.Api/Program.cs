using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using CleanHouse.Api.Middlewares;
using CleanHouse.Data.DbContexts;
using CleanHouse.Service.Helpers;
using CleanHouse.Service.Mappers;
using CleanHouse.Api.Extensions;
using CleanHouse.Api.Models;

namespace CleanHouse.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<CleanHouseDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AAA", builder => builder.WithOrigins("https://82.215.96.174").AllowAnyHeader().AllowAnyMethod());
        //});

        //JWT
        builder.Services.AddJwtService(builder.Configuration);
        //Swagger
        builder.Services.AddSwaggerService();


        builder.Services.AddCustomServices();
        builder.Services.AddAutoMapper(typeof(MapperProfile));

        //Configure api url name
        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(
                                                new ConfigurationApiUrlName()));
        });


        var app = builder.Build();

        // Getting full path of wwwroot
        WebHostEnviromentHelper.WebRootPath = Path.GetFullPath("wwwroot");


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}