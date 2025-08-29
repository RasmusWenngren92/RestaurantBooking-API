using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories.IRepositores;

namespace RestaurantBookingAPI.Repositories
{
    public class TableRepository(RestaurantDBContext context) : ITableRepository
    {
        private readonly RestaurantDBContext _context = context;

        public async Task<Table?> GetTableByIdAsync(int tableId)
        {
            return await _context.Tables
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == tableId);
        }
        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }
        public async Task<Table> CreateTableAsync(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table;
        }
        public async Task<Table> UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
            return table;
        }
        public async Task<bool> DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null)
                return false;

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            return await _context.Tables
                .Where(t => t.IsAvailable)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetTablesByCapacityAsync(int minCapacity)
        {
            return await _context.Tables
                .Where(t => t.SeatingCapacity >= minCapacity)
                .OrderBy(t => t.SeatingCapacity)
                .ThenBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<Table?> GetTableByNumberAsync(int tableNumber)
        {
            return await _context.Tables
                .FirstOrDefaultAsync(t => t.TableNumber == tableNumber);
        }

        public async Task<bool> TableNumberExistsAsync(int tableNumber)
        {
            return await _context.Tables
                .AnyAsync(t => t.TableNumber == tableNumber);
        }

        public async Task<IEnumerable<Table>> GetTablesSuitableForPartySizeAsync(int partySize)
        {
            return await _context.Tables
                .Where(t => t.IsAvailable && t.SeatingCapacity >= partySize)
                .OrderBy(t => t.SeatingCapacity) 
                .ThenBy(t => t.TableNumber)
                .ToListAsync();
        }
        public async Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null)
                return false;

            table.IsAvailable = isAvailable;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
