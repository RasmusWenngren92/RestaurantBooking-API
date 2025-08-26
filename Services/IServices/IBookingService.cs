using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface IBookingService
    {
       
        Task<BookingDTO?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();

       
        Task<IEnumerable<BookingDTO>> GetBookingsByCustomerAsync(int customerId);
        Task<IEnumerable<BookingDTO>> GetBookingsByDateAsync(DateTime date);

        
        Task<BookingDTO> CreateBookingAsync(CreateBookingDTO createBookingDto);
        Task<BookingDTO> UpdateBookingAsync(int id, UpdateBookingDTO updateBookingDto);
        Task<bool> CancelBookingAsync(int id);
        Task<bool> DeleteBookingAsync(int id);

       
        Task<AvailabilityResponseDTO> CheckAvailabilityAsync(AvailabilityRequestDTO availabilityRequest);
        Task<bool> IsTimeSlotAvailableAsync(DateTime requestedTime, int partySize);

     
        Task<IEnumerable<int>> GetAllAvailableTablesAsync(DateTime requestedTime, int partySize);

        
        Task<IEnumerable<BookingSummaryDTO>> GetTodayBookingsAsync(); 
        Task<IEnumerable<BookingSummaryDTO>> GetActiveBookingsAsync();
        Task<IEnumerable<BookingSummaryDTO>> GetUpcomingBookingsAsync(int days = 7);

        
        Task<bool> MarkBookingAsCompletedAsync(int id);
        Task<bool> MarkBookingAsNoShowAsync(int id);

        
        Task<bool> ValidateBookingTime(DateTime requestedTime); // Non-async version
        Task<bool> CanModifyBookingAsync(int bookingId);
    }
}
