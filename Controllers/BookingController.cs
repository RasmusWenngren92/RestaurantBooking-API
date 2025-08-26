using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services.IServices;
using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IBookingService bookingService, IConfiguration configuration) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;
        private readonly IConfiguration _configuration = configuration;
        // Define your endpoints here, e.g., GetBookings, CreateBooking, UpdateBooking, DeleteBooking

        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<List<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);                                                                       
        }
        [HttpGet("GetBookingById/{bookingId}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        [HttpPost("CreateBooking")]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            if (createBookingDTO == null)
            {
                return BadRequest("Booking cannot be null");
            }
            var bookingId = await _bookingService.CreateBookingAsync(createBookingDTO);
            return CreatedAtAction(nameof(GetBookingById), new { bookingId = bookingId }, bookingId);
        }
        [HttpPut("UpdateBooking")]
        public async Task<ActionResult<bool>> UpdateBooking(UpdateBookingDTO updateBookingDTO, int id)
        {
            if (updateBookingDTO == null)
            {
                return BadRequest("Booking cannot be null");
            }
            var result = await _bookingService.UpdateBookingAsync(id, updateBookingDTO);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(true);
        }
        [HttpDelete("DeleteBooking/{bookingId}")]
        public async Task<ActionResult<bool>> DeleteBooking(int bookingId)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingId);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
