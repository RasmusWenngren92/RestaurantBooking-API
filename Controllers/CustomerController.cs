using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [Authorize]
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
        [Authorize]
        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            return Ok(customer);
        }
        [HttpPost("AddCustomer")]
        public async Task<ActionResult<bool>> AddCustomer(CreateCustomerDTO customerDTO)
        {
            var result = await _customerService.AddCustomerAsync(customerDTO);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetCustomerWithBookingCount/{customerId}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerWithBookingCount(int customerId)
        {
            var customer = await _customerService.GetCustomerWithBookingCountAsync(customerId);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [Authorize]
        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<bool>> UpdateCustomer(UpdateCustomerDTO customerDTO)
        {
            var result = await _customerService.UpdateCustomerAsync(customerDTO);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<ActionResult<bool>> DeleteCustomer(int customerId)
        {
            var result = await _customerService.DeleteCustomerAsync(customerId);
            return Ok(result);
        }
    }
}
