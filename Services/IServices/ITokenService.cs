using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface ITokenService
    {
        string GenerateToken(Admin admin);
    }
}
