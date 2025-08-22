using System.ComponentModel.DataAnnotations;

namespace RestauantBookingAPI.DTOs
{
    public class MenuItemDTO
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Menu Item Name")]
        public string? Name { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Menu Item Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        [Display(Name = "Price (sek)", Description = "Price in Swedish Krona (sek)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
        [Display(Name = "Food Category", Description = "Type of cousine (e.g., Appetizer, Main Course)")]
        public string? Category { get; set; }
        [Display(Name = "Is Popular", Description = "Indicates if the menu item is popular")]
        public bool IsPopular { get; set; }
        [Url(ErrorMessage = "Invalid URL format.")]
        [Display(Name = "Image URL", Description = "URL of the menu item image")]
        public string? ImageUrl { get; set; } 
        // Additional properties can be added as needed
    }
}
