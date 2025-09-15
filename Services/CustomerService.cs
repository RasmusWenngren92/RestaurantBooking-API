using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Mappers;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories;
using RestaurantBookingAPI.Repositories.IRepositores;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingRepository _bookingRepository;

        public CustomerService(ICustomerRepository customerRepository, IBookingRepository bookingRepository)
        {
            _customerRepository = customerRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(DomainMapper.ToCustomerDTO);
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            return customer != null ? DomainMapper.ToCustomerDTO(customer) : null;
        }
        public async Task<CustomerDTO?> GetCustomerWithBookingCountAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null) return null;

            // Expensive calculation only when specifically requested
            var bookingCount = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
            var totalBookings = bookingCount.Count();

            var customerDto = DomainMapper.ToCustomerDTO(customer);
            customerDto.TotalBookings = totalBookings;
            return customerDto;
        }

        public async Task<CustomerDTO> AddCustomerAsync(CreateCustomerDTO customerDTO)
        {
            if (customerDTO == null)
                throw new ArgumentException("Customer data is required");

            if (string.IsNullOrEmpty(customerDTO.FirstName))
                throw new ArgumentException("First name is required");

            if (string.IsNullOrEmpty(customerDTO.LastName))
                throw new ArgumentException("Last name is required");

            if (string.IsNullOrEmpty(customerDTO.Email))
                throw new ArgumentException("Email is required");
            var customer = DomainMapper.ToCustomer(customerDTO);
            var customerId = await _customerRepository.AddCustomerAsync(customer);

            var createdCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
            return DomainMapper.ToCustomerDTO(createdCustomer!);
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(int id, UpdateCustomerDTO customerDTO)
        {
            if (customerDTO == null)
                throw new ArgumentException("Customer data is required");

            if (customerDTO.Id <= 0)
                throw new ArgumentException("Invalid customer ID");

            if (string.IsNullOrEmpty(customerDTO.FirstName))
                throw new ArgumentException("First name is required");

            var customer = DomainMapper.ToCustomer(customerDTO);
            var updatedCustomer = await _customerRepository.UpdateCustomerAsync(customer);
            return DomainMapper.ToCustomerDTO(updatedCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Invalid customer ID");

            return await _customerRepository.DeleteCustomerAsync(customerId);
        }

        public async Task<CustomerDTO?> GetCustomerByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email is required");
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            return customer != null ? DomainMapper.ToCustomerDTO(customer) : null;
        }
    }
}
