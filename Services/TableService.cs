using RestauantBookingAPI.Models.Enums;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories.IRepositores;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IBookingRepository _bookingRepository;
        public TableService(ITableRepository tableRepository, IBookingRepository bookingRepository)
        {
            _tableRepository = tableRepository;
            _bookingRepository = bookingRepository;
        }
        private async Task<TableDTO> MapToTableDTOAsync(Table table)
        {
            
            var activeBookings = await _bookingRepository.GetActiveBookingsAsync();
            var currentBookings = activeBookings.Count(b => b.TableId == table.Id);

            return new TableDTO
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                SeatingCapacity = table.SeatingCapacity,
                IsAvailable = table.IsAvailable,
                CurrentBookings = currentBookings
            };
        }
        public async Task<TableDTO?> GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetTableByIdAsync(id);
            return table != null ? await MapToTableDTOAsync(table) : null;
        }

        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTablesAsync();
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(await MapToTableDTOAsync(table));
            }

            return tableDtos;
        }
        public async Task<TableDTO> CreateTableAsync(CreateTableDTO createTableDto)
        {
            // Validate table number doesn't exist
            if (await _tableRepository.TableNumberExistsAsync(createTableDto.TableNumber))
            {
                throw new ArgumentException($"Table number {createTableDto.TableNumber} already exists.");
            }

            var table = new Table
            {
                TableNumber = createTableDto.TableNumber,
                SeatingCapacity = createTableDto.SeatingCapacity,
                IsAvailable = createTableDto.IsAvailable
            };

            var createdTable = await _tableRepository.CreateTableAsync(table);
            return await MapToTableDTOAsync(createdTable);
        }

        public async Task<TableDTO> UpdateTableAsync(int id, UpdateTableDTO updateTableDto)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(id);
            if (existingTable == null)
            {
                throw new ArgumentException($"Table with ID {id} not found.");
            }

            
            if (existingTable.TableNumber != updateTableDto.TableNumber)
            {
                if (await _tableRepository.TableNumberExistsAsync(updateTableDto.TableNumber))
                {
                    throw new ArgumentException($"Table number {updateTableDto.TableNumber} already exists.");
                }
            }

            existingTable.TableNumber = updateTableDto.TableNumber;
            existingTable.SeatingCapacity = updateTableDto.SeatingCapacity;
            existingTable.IsAvailable = updateTableDto.IsAvailable;

            var updatedTable = await _tableRepository.UpdateTableAsync(existingTable);
            return await MapToTableDTOAsync(updatedTable);
        }
        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _tableRepository.GetTableByIdAsync(id);
            if (table == null) return false;

            // Check if table has active bookings
            var activeBookings = await _bookingRepository.GetActiveBookingsAsync();
            var hasActiveBookings = activeBookings.Any(b => b.TableId == id);

            if (hasActiveBookings)
            {
                throw new InvalidOperationException("Cannot delete table with active bookings.");
            }

            return await _tableRepository.DeleteTableAsync(id);
        }
        public async Task<IEnumerable<TableSummaryDTO>> GetTablesSummaryAsync()
        {
            var tables = await _tableRepository.GetAllTablesAsync();
            var summaries = new List<TableSummaryDTO>();

            foreach (var table in tables)
            {
                var status = await GetTableStatusAsync(table.Id);
                summaries.Add(new TableSummaryDTO
                {
                    Id = table.Id,
                    TableNumber = table.TableNumber,
                    SeatingCapacity = table.SeatingCapacity,
                    IsAvailable = table.IsAvailable,
                    Status = status
                });
            }

            return summaries.OrderBy(t => t.TableNumber);
        }

        public async Task<IEnumerable<TableDTO>> GetAvailableTablesAsync()
        {
            var tables = await _tableRepository.GetAvailableTablesAsync();
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(await MapToTableDTOAsync(table));
            }

            return tableDtos;
        }

        public async Task<IEnumerable<TableDTO>> GetTablesByCapacityAsync(int minCapacity)
        {
            var tables = await _tableRepository.GetTablesByCapacityAsync(minCapacity);
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(await MapToTableDTOAsync(table));
            }

            return tableDtos;
        }
        public async Task<TableDTO?> GetTableByNumberAsync(int tableNumber)
        {
            var table = await _tableRepository.GetTableByNumberAsync(tableNumber);
            return table != null ? await MapToTableDTOAsync(table) : null;
        }

        public async Task<IEnumerable<TableDTO>> GetTablesSuitableForPartySizeAsync(int partySize)
        {
            var tables = await _tableRepository.GetTablesSuitableForPartySizeAsync(partySize);
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(await MapToTableDTOAsync(table));
            }

            return tableDtos;
        }
        public async Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable)
        {
            // If setting to unavailable, check for active bookings
            if (!isAvailable)
            {
                var activeBookings = await _bookingRepository.GetActiveBookingsAsync();
                var hasActiveBookings = activeBookings.Any(b => b.TableId == tableId);

                if (hasActiveBookings)
                {
                    throw new InvalidOperationException("Cannot set table as unavailable when it has active bookings.");
                }
            }

            return await _tableRepository.SetTableAvailabilityAsync(tableId, isAvailable);
        }

        public async Task<bool> TableNumberExistsAsync(int tableNumber)
        {
            return await _tableRepository.TableNumberExistsAsync(tableNumber);
        }

        public async Task<string> GetTableStatusAsync(int tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            if (table == null) return "Not Found";

            if (!table.IsAvailable) return "Unavailable";

           
            var now = DateTime.Now;
            var currentBookings = await _bookingRepository.GetBookingsInTimeRangeAsync(
                now.AddHours(-2), 
                now.AddHours(2)   
            );

            var isCurrentlyBooked = currentBookings.Any(b =>
                b.TableId == tableId &&
                b.Status == BookingStatus.Confirmed &&
                b.StartDateTime <= now &&
                b.StartDateTime.AddHours(2) > now);

            return isCurrentlyBooked ? "Occupied" : "Available";
        }
    }
}
