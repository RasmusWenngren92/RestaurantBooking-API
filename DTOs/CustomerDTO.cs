using System.ComponentModel.DataAnnotations;

namespace RestauantBookingAPI.DTOs
{
    public class CustomerDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [Display(Name = "First Name", Description = "Customer's first name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [Display(Name = "Last Name", Description = "Customer's last name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address", Description = "Customer's email address")]
        public string? Email { get; set; }
        [Required]
        [RegularExpression(@"\+46|0[7][0-9]{8}", ErrorMessage = "Invalid phone number format. Must start with +46 or 07 followed by 8 digits.")]
        public string? PhoneNumber { get; set; }
        // Additional properties can be added here if needed
        [Display(Name = "Is Active", Description = "Indicates if the customer is active")]
        public bool IsActive { get; set; } = true;
    }
}
