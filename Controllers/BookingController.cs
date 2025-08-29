using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Services;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<List<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);                                                                       
        }
        [Authorize]
        [HttpGet("GetBookingById/{bookingId}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            return Ok(booking);
        }
        [HttpPost("CreateBooking")]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var createdBooking = await _bookingService.CreateBookingAsync(createBookingDTO);
            return CreatedAtAction(nameof(GetBookingById), new { bookingId = createdBooking.Id }, createdBooking);
        }
        [Authorize]
        [HttpPut("UpdateBooking")]
        public async Task<ActionResult<bool>> UpdateBooking(UpdateBookingDTO updateBookingDTO, int id)
        {
            var updatedBooking = await _bookingService.UpdateBookingAsync(id, updateBookingDTO);
            return Ok(updatedBooking);
        }
        [Authorize]
        [HttpDelete("DeleteBooking/{bookingId}")]
        public async Task<ActionResult<bool>> DeleteBooking(int bookingId)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingId);
            return Ok(result);
        }
    }
}
