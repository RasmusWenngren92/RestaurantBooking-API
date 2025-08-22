using Microsoft.EntityFrameworkCore;
using RestauantBookingAPI.Data;
using RestauantBookingAPI.Models.Entities;
using RestauantBookingAPI.Repositories.IRepositores;

namespace RestauantBookingAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantDBContext _context;
        public CustomerRepository(RestaurantDBContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
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
    }
}
