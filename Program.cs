
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaruantBookingAPI.Services;
using RestauantBookingAPI.Middleware;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.Repositories;
using RestaurantBookingAPI.Repositories.IRepositores;
using RestaurantBookingAPI.Services;
using RestaurantBookingAPI.Services.IServices;


namespace RestaurantBookingAPI
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
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddControllers();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(x => new {
                            field = x.Key,
                            message = x.Value.Errors.First().ErrorMessage
                        });

                    var response = new
                    {
                        message = errors.First().message,
                        type = "Validation Error",
                        field = errors.First().field
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };

                });
            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseGlobalExceptionMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
