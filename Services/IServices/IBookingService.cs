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

        Task<IEnumerable<BookingSummaryDTO>> GetTodayBookingsAsync(); 
        Task<IEnumerable<BookingSummaryDTO>> GetActiveBookingsAsync();
        Task<IEnumerable<BookingSummaryDTO>> GetUpcomingBookingsAsync(int days = 7);

        
        Task<bool> MarkBookingAsCompletedAsync(int id);
        Task<bool> MarkBookingAsNoShowAsync(int id);

       
        Task<bool> CanModifyBookingAsync(int bookingId);
    }
}
