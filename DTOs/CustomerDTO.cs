using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingAPI.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
    }
    public class CustomerSummaryDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int ActiveBookings { get; set; } 
    }
    public class CreateCustomerDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [Display(Name = "First Name", Description = "Customer's first name")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [Display(Name = "Last Name", Description = "Customer's last name")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address", Description = "Customer's email address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"\+46|0[7][0-9]{8}", ErrorMessage = "Invalid phone number format. Must start with +46 or 07 followed by 8 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
    public class UpdateCustomerDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [Display(Name = "First Name", Description = "Customer's first name")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [Display(Name = "Last Name", Description = "Customer's last name")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [Display(Name = "Email Address", Description = "Customer's email address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"\+46|0[7][0-9]{8}", ErrorMessage = "Invalid phone number format. Must start with +46 or 07 followed by 8 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
