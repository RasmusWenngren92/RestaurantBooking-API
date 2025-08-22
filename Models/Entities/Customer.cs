namespace RestauantBookingAPI.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; }           = null!;
        public string Email { get; set; }                       = null!;
        public string PhoneNumber { get; set; }                          = null!;
        public bool IsActive { get; set; } = true;


    }
}
