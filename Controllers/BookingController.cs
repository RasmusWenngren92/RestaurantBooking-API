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

        
        [HttpGet]
        public async Task<ActionResult<List<BookingDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);                                                                       
        }
        [Authorize]
        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            return Ok(booking);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var createdBooking = await _bookingService.CreateBookingAsync(createBookingDTO);
            return CreatedAtAction(nameof(GetBookingById), new { bookingId = createdBooking.Id }, createdBooking);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateBooking(int id, UpdateBookingDTO updateBookingDTO)
        {
            var updatedBooking = await _bookingService.UpdateBookingAsync(id, updateBookingDTO);
            return Ok(updatedBooking);
        }
        [Authorize]
        [HttpDelete("{bookingId}")]
        public async Task<ActionResult<bool>> DeleteBooking(int bookingId)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingId);
            return Ok(result);
        }
    }
}
