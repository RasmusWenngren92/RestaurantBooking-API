﻿namespace RestaurantBookingAPI.Models.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Role { get; set; } = "Admin";
    }
}
