using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);
        Task<bool> AddCustomerAsync(CreateCustomerDTO customerDTO);
        Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDTO customerDTO);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<CustomerDTO?> GetCustomerWithBookingCountAsync(int customerId);
    }
}
