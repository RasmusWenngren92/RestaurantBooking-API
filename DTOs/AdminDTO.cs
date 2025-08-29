namespace RestaurantBookingAPI.DTOs
{
    public class AdminRegisterDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
    public class LoginAdminDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }

}
