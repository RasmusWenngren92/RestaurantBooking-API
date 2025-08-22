
using Microsoft.EntityFrameworkCore;
using RestauantBookingAPI.Data;
using RestauantBookingAPI.Repositories;
using RestauantBookingAPI.Repositories.IRepositores;
using RestauantBookingAPI.Services.IServices;
using RestauantBookingAPI.Services;

namespace RestauantBooking_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<RestaurantDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IMenuItemService, MenuItemService>();
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
