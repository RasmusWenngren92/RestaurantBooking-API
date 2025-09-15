namespace RestaurantBookingAPI.DTOs
{
    public class AdminRegisterDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
    public class LoginAdminDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }

}
