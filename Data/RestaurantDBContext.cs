using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Data
{
    public class RestaurantDBContext : DbContext

    {
        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   

           
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
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
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TableNumber).IsRequired();
                entity.Property(e => e.SeatingCapacity).IsRequired();
                entity.Property(e => e.IsAvailable).HasDefaultValue(true).IsRequired();
                entity.HasIndex(e => e.TableNumber).IsUnique();
                entity.HasIndex(e => e.SeatingCapacity)
                .HasDatabaseName("IX_Tables_SeatingCapacity");
                entity.HasIndex(e => e.IsAvailable)
                .HasDatabaseName("IX_Tables_IsAvailable");
            });
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.StartDateTime)
                    .IsRequired();

                entity.Property(e => e.NumberOfGuests)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Bookings)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bookings_Customers");

                entity.HasOne(e => e.Table)
                    .WithMany(t => t.Bookings)
                    .HasForeignKey(e => e.TableId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bookings_Tables");

                entity.HasIndex(e => e.CustomerId)
                    .HasDatabaseName("IX_Bookings_CustomerId");

                entity.HasIndex(e => e.TableId)
                    .HasDatabaseName("IX_Bookings_TableId");

                entity.HasIndex(e => e.StartDateTime)
                    .HasDatabaseName("IX_Bookings_StartDateTime");

                entity.HasIndex(e => e.Status)
                    .HasDatabaseName("IX_Bookings_Status");

                
                entity.HasIndex(e => new { e.TableId, e.StartDateTime, e.Status })
                    .HasDatabaseName("IX_Bookings_AvailabilityCheck")
                    .HasFilter("[Status] != 'Cancelled'");

                
                entity.HasIndex(e => new { e.CustomerId, e.StartDateTime })
                    .HasDatabaseName("IX_Bookings_CustomerBookings");
            });
            modelBuilder.Entity<Admin>(entity =>
            {
                // Primary Key
                entity.HasKey(a => a.Id);

                entity.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(a => a.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Admin_Email"); 

                entity.Property(a => a.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Admin"); 
            });

        }
    }
   
    
}

