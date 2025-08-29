using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> LoginAsync(LoginAdminDTO loginDto);
        Task<bool> RegisterAsync(AdminRegisterDTO registerDto);
    }
}
