using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Models.Enums;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories.IRepositores;

namespace RestaurantBookingAPI.Repositories
{
    public class BookingRepository(RestaurantDBContext context) : IBookingRepository
    {
        private readonly RestaurantDBContext _context = context;

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.StartDateTime >= startOfDay && b.StartDateTime < endOfDay)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.CustomerId == customerId)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }
        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            booking.CreatedAt = DateTime.UtcNow;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            booking.UpdatedAt = DateTime.UtcNow;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> FindFirstAvailableTableAsync(DateTime requestedTime, int partySize)
        {
            // Calculate the 4-hour blocking window
            var blockStart = requestedTime.AddHours(-2);  // 2 hours before
            var blockEnd = requestedTime.AddHours(2);     // 2 hours after

            // Find smallest suitable table that's available
            var availableTable = await _context.Tables
                .Where(t => t.IsAvailable && t.SeatingCapacity >= partySize)
                .OrderBy(t => t.SeatingCapacity) // Smallest suitable table first
                .FirstOrDefaultAsync(t => !_context.Bookings
                    .Any(b => b.TableId == t.Id
                           && b.Status != BookingStatus.Cancelled
                           && b.StartDateTime > blockStart     // Exclusive boundaries
                           && b.StartDateTime < blockEnd));

            return availableTable?.Id;
        }
        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime requestedTime)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null || !table.IsAvailable) return false;

            var blockStart = requestedTime.AddHours(-2);
            var blockEnd = requestedTime.AddHours(2);

            var hasConflict = await _context.Bookings
                .AnyAsync(b => b.TableId == tableId
                            && b.Status != BookingStatus.Cancelled
                            && b.StartDateTime > blockStart
                            && b.StartDateTime < blockEnd);

            return !hasConflict;
        }
        public async Task<IEnumerable<int>> GetAllAvailableTablesAsync(DateTime requestedTime, int partySize)
        {
            var blockStart = requestedTime.AddHours(-2);
            var blockEnd = requestedTime.AddHours(2);

            return await _context.Tables
                .Where(t => t.IsAvailable && t.SeatingCapacity >= partySize)
                .Where(t => !_context.Bookings
                    .Any(b => b.TableId == t.Id
                           && b.Status != BookingStatus.Cancelled
                           && b.StartDateTime > blockStart
                           && b.StartDateTime < blockEnd))
                .OrderBy(t => t.SeatingCapacity)
                .Select(t => t.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.Status == BookingStatus.Confirmed
                         && b.StartDateTime > now) 
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsInTimeRangeAsync(DateTime startTime, DateTime endTime)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.StartDateTime >= startTime && b.StartDateTime <= endTime)
                .OrderBy(b => b.StartDateTime)
                .ToListAsync();
        }
        public async Task<bool> HasConflictingBookingAsync(int tableId, DateTime requestedTime, int? excludeBookingId = null)
        {
            var blockStart = requestedTime.AddHours(-2);
            var blockEnd = requestedTime.AddHours(2);
            var query = _context.Bookings
                .Where(b => b.TableId == tableId
                         && b.Status != BookingStatus.Cancelled
                         && b.StartDateTime > blockStart
                         && b.StartDateTime < blockEnd);
            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }
            return await query.AnyAsync();
        }
 
    }
}
