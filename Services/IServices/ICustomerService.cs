using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);
        Task<CustomerDTO> AddCustomerAsync(CreateCustomerDTO customerDTO);
        Task<CustomerDTO> UpdateCustomerAsync(int id, UpdateCustomerDTO customerDTO);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<CustomerDTO?> GetCustomerWithBookingCountAsync(int customerId);
        Task<CustomerDTO?> GetCustomerByEmailAsync(string email);
    }
}
