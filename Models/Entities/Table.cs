namespace RestaurantBookingAPI.Models.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SeatingCapacity { get; set; }
        public bool IsAvailable { get; set; }

        public List<Booking> Bookings { get; set; } = [];
    }
}
