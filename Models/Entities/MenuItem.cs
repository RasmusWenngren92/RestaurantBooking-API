namespace RestauantBookingAPI.Models.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get;  set; }
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public bool IsPopular { get; set; }
        public string ImageUrl { get; set; }   = null!;
    }
}
