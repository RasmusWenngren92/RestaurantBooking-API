using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Repositories.IRepositores
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateTime date);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<int?> FindFirstAvailableTableAsync(DateTime requestedTime, int partySize);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime requestedTime);
        Task<IEnumerable<int>> GetAllAvailableTablesAsync(DateTime requestedTime, int partySize);

        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsInTimeRangeAsync(DateTime startTime, DateTime endTime);
        Task<bool> HasConflictingBookingAsync(int tableId, DateTime requestedTime, int? excludeBookingId = null);
        
    }
}
