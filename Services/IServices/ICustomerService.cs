using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);
        Task<int> AddCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO);
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}
