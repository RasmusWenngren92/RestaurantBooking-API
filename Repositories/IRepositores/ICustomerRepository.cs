using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Repositories.IRepositores
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<int> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}
