using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingAPI.DTOs
{
    public class MenuItemDTO
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
        public string? Category { get; set; }
        [Display(Name = "Is Popular", Description = "Indicates if the menu item is popular")]
        public bool IsPopular { get; set; }
        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get; set; } 
        // Additional properties can be added as needed
    }
}
