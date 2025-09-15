namespace RestaurantBookingAPI.Models.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Role { get; set; } = "Admin";

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
