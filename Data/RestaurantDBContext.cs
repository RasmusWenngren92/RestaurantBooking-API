using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Data
{
    public class RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : DbContext(options)

    {
        //public DbSet<Admin> Admins { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            var tableEntity = modelBuilder.Entity<Table>();
            //var adminEntity = modelBuilder.Entity<Admin>();
            var bookingEntity = modelBuilder.Entity<Booking>();   
            var menuItemEntity = modelBuilder.Entity<MenuItem>();
            var customerEntity = modelBuilder.Entity<Customer>();
           
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IsPopular).HasDefaultValue(false);

                entity.HasIndex(e => e.Category);
                entity.HasIndex(e => e.IsPopular);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.IsActive).HasDefaultValue(true)
                    .IsRequired();
            });
        }
    }
   
    
}

