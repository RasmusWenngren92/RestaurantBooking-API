using System.ComponentModel.DataAnnotations;
using RestaurantBookingAPI.Models.Enums;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.DTOs

{
   
    public class CreateBookingDTO
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Booking date is required.")]
        public DateTime StartDateTime { get; set; }
        [Required(ErrorMessage = "Number of guests is required.")]
        [Range(1, 8, ErrorMessage = "Party size must be between 1-8. For larger parties, please call the restaurant.")]
        public int NumberOfGuests { get; set; }
    }
    public class UpdateBookingDTO
    {
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required(ErrorMessage = "Number of guests is required.")]
        [Range(1, 8, ErrorMessage = "Party size must be between 1-8. For larger parties, please call the restaurant.")]
        public int NumberOfGuests { get; set; }

        public BookingStatus Status { get; set; }

    }
    public class BookingDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime StartDateTime { get; set; }
        public int NumberOfGuests { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public int? TableNumber { get; set; }
        public int? TableCapacity { get; set; }
    }
    public class AvailabilityRequestDTO
    {
        [Required(ErrorMessage = "Booking date is required.")]
        public DateTime BookingDate { get; set; }
        [Required(ErrorMessage = "Number of guests is required.")]
        [Range(1, 8, ErrorMessage = "Party size must be between 1-8. For larger parties, please call the restaurant.")]
        public int NumberOfGuests { get; set; }
    }
    public class AvailabilityResponseDTO
    {
        public bool IsAvailable { get; set; }
        public int? AvailableTableId { get; set; }
        public int? TableNumber { get; set; }
        public List<int> AllAvailableTableIds { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }
    public class BookingSummaryDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int TableNumber { get; set; }
        public DateTime StartDateTime { get; set; }
        public int NumberOfGuests { get; set; }
        public BookingStatus Status { get; set; }
        }; 

    }

