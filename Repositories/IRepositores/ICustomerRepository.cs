using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Repositories.IRepositores
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<int> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customerDTO);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}
