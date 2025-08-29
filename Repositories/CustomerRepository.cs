using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories.IRepositores;

namespace RestaurantBookingAPI.Repositories
{
    public class CustomerRepository(RestaurantDBContext context) : ICustomerRepository
    {
        private readonly RestaurantDBContext _context = context;

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            var result = await _context.SaveChangesAsync();
            if(result != 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var rowsAffected = await _context.Customers
                .Where(c => c.Id == customerId)
                .ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
