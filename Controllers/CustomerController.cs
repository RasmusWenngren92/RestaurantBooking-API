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
        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers() =>
        Ok(await _customerService.GetAllCustomersAsync());

        [Authorize]
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int customerId) =>
        Ok(await _customerService.GetCustomerByIdAsync(customerId));

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> AddCustomer([FromBody] CreateCustomerDTO dto) =>
        Ok(await _customerService.AddCustomerAsync(dto));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(int id, UpdateCustomerDTO customerDTO)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customerDTO);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{customerId}")]
        public async Task<ActionResult<bool>> DeleteCustomer(int customerId) =>
        Ok(await _customerService.DeleteCustomerAsync(customerId));
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerByEmail([FromQuery] string email) =>
            Ok(await _customerService.GetCustomerByEmailAsync(email));
    }
}
