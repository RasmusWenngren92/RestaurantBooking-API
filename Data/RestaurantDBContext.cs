using Microsoft.EntityFrameworkCore;
using RestauantBookingAPI.Models.Entities;

namespace RestauantBookingAPI.Data
{
    public class RestaurantDBContext : DbContext

    {
        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminEntity = modelBuilder.Entity<Admin>();

            var bookingEntity = modelBuilder.Entity<Booking>();

            var menuEntity = modelBuilder.Entity<MenuItem>();

            var customerEntity = modelBuilder.Entity<Customer>();
        }
    }
   
    
}

