using RestauantBookingAPI.Models.Enums;

namespace RestaurantBookingAPI.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }

        public DateTime StartDateTime { get; set; }
        public int NumberOfGuests { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public DateTime EndDateTime => StartDateTime.AddHours(2);
        public virtual Customer? Customer { get; set; }
        public virtual Table? Table { get; set; }
    }
}
