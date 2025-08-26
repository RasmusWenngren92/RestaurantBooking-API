using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService, IConfiguration configuration) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;
        private readonly IConfiguration _configuration = configuration;
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost("AddCustomer")]
        public async Task<ActionResult<int>> AddCustomer(CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest("Customer cannot be null");
            }
            var customerId = await _customerService.AddCustomerAsync(customerDTO);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customerId }, customerId);
        }
        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<bool>> UpdateCustomer(CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest("Customer cannot be null");
            }
            var result = await _customerService.UpdateCustomerAsync(customerDTO);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<ActionResult<bool>> DeleteCustomer(int customerId)
        {
            var result = await _customerService.DeleteCustomerAsync(customerId);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
