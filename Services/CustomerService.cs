using RestauantBookingAPI.DTOs;
using RestauantBookingAPI.Models.Entities;
using RestauantBookingAPI.Repositories.IRepositores;
using RestauantBookingAPI.Services.IServices;

namespace RestauantBookingAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            var customerDTO = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            }).ToList();
            return customerDTO;
        }
        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return null;
            }
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }
        public async Task<int> AddCustomerAsync(CustomerDTO customerDTO)
        {
            ArgumentNullException.ThrowIfNull(customerDTO);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.FirstName);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.LastName);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.Email);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.PhoneNumber);
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber
            };
            return await _customerRepository.AddCustomerAsync(customer);
        }
        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO)
        {
            ArgumentNullException.ThrowIfNull(customerDTO);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.FirstName);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.LastName);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.Email);
            ArgumentException.ThrowIfNullOrEmpty(customerDTO.PhoneNumber);
            var customer = new Customer
            {
                Id = customerDTO.Id,
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber
            };
            return await _customerRepository.UpdateCustomerAsync(customer);
        }
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Invalid customer ID");
            }
            return await _customerRepository.DeleteCustomerAsync(customerId);
        }
    }
}
