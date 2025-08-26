using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingAPI.DTOs
{
    public class TableDTO
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SeatingCapacity { get; set; }
        public bool IsAvailable { get; set; }
        public int CurrentBookings { get; set; }
    }
    public class CreateTableDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Table number must be a positive integer.")]
        public int TableNumber { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Seating capacity must be between 1 and 20.")]
        public int SeatingCapacity { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
    public class UpdateTableDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Table number must be a positive integer.")]
        public int TableNumber { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Seating capacity must be between 1 and 20.")]
        public int SeatingCapacity { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
    public class TableSummaryDTO
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SeatingCapacity { get; set; }
        public bool IsAvailable { get; set; }
        public string Status { get; set; } = string.Empty;
    }
    public class SetTableAvailabilityDTO
    {
        [Required]
        public bool IsAvailable { get; set; }
    }
}
