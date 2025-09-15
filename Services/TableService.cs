using RestaurantBookingAPI.Models.Enums;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Mappers;
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

        public async Task<TableDTO?> GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetTableByIdAsync(id);
            return table != null ? DomainMapper.ToTableDTO(table) : null;
        }

        public async Task<IEnumerable<TableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllTablesAsync();
            return tables.Select(DomainMapper.ToTableDTO);
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
            return DomainMapper.ToTableDTO(createdTable);
        }
        public async Task<TableDTO?> GetTableWithBookingCountAsync(int id)
        {
            var table = await _tableRepository.GetTableByIdAsync(id);
            if (table == null) return null;

            var todayBookings = await _bookingRepository.GetBookingsByDateAsync(DateTime.Today);
            var bookingCount = todayBookings.Count(b => b.TableId == id);

            return DomainMapper.ToTableDTO(table, bookingCount);
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
            return DomainMapper.ToTableDTO(updatedTable);
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
            var now = DateTime.Now;
            var relevantBookings = await _bookingRepository.GetBookingsInTimeRangeAsync(
                now.AddHours(-2), now.AddHours(2));

            return tables.Select(table =>
            {
                var status = CalculateTableStatus(table, relevantBookings.Where(b => b.TableId == table.Id), now);
                return DomainMapper.ToTableSummaryDTO(table, status);
            }).OrderBy(t => t.TableNumber);
        }

        private string CalculateTableStatus(Table table, IEnumerable<Booking> relevantBookings, DateTime now)
        {
            if (!table.IsAvailable) return "Unavailable";

            var isCurrentlyBooked = relevantBookings.Any(b =>
                b.Status == BookingStatus.Confirmed &&
                b.StartDateTime <= now &&
                b.StartDateTime.AddHours(2) > now);

            return isCurrentlyBooked ? "Occupied" : "Available";
        }

        public async Task<IEnumerable<TableDTO>> GetAvailableTablesAsync()
        {
            var tables = await _tableRepository.GetAvailableTablesAsync();
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(DomainMapper.ToTableDTO(table));
            }

            return tableDtos;
        }

        public async Task<IEnumerable<TableDTO>> GetTablesByCapacityAsync(int minCapacity)
        {
            var tables = await _tableRepository.GetTablesByCapacityAsync(minCapacity);
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(DomainMapper.ToTableDTO(table));
            }

            return tableDtos;
        }
        public async Task<TableDTO?> GetTableByNumberAsync(int tableNumber)
        {
            var table = await _tableRepository.GetTableByNumberAsync(tableNumber);
            return table != null ? DomainMapper.ToTableDTO(table) : null;
        }

        public async Task<IEnumerable<TableDTO>> GetTablesSuitableForPartySizeAsync(int partySize)
        {
            var tables = await _tableRepository.GetTablesSuitableForPartySizeAsync(partySize);
            var tableDtos = new List<TableDTO>();

            foreach (var table in tables)
            {
                tableDtos.Add(DomainMapper.ToTableDTO(table));
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
        

        public async Task<(bool IsValid, string ErrorMessage)> ValidateBookingTimeAsync(DateTime dateTime, int partySize)
        {
            if (partySize < 1 || partySize > 8)
            {
                return (false, "Party size must be between 1 and 8. For larger parties, please call the restaurant.");
            }
            if (dateTime <= DateTime.Now.AddHours(1))
            {
                return (false, "Booking must be made at least 1 hour in advance.");
            }
            var hour = dateTime.Hour;
            if (hour < 10 || hour >= 22)
            {
                return (false, "Restaurant is open from 10:00 AM to 10:00 PM.");
            }
            if (dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return (false, "Restaurant is closed on Sundays.");
            }
            return (true, string.Empty);
        }

        public async Task<(bool IsAvailable, int? FirstAvailableTableId, IEnumerable<int> AllAvailableTableIds)> CheckTableAvailabilityAsync(DateTime dateTime, int partySize)
        {
            var firstAvailable = await _bookingRepository.FindFirstAvailableTableAsync(dateTime, partySize);
            var allAvailable = await _bookingRepository.GetAllAvailableTablesAsync(dateTime, partySize);

            return (firstAvailable.HasValue, firstAvailable, allAvailable);
        }

        public async Task<AvailabilityResponseDTO> GetTableAvailabilityAsync(AvailabilityRequestDTO availabilityRequest)
        {
            var validation = await ValidateBookingTimeAsync(availabilityRequest.BookingDate, availabilityRequest.NumberOfGuests);
            if (!validation.IsValid)
            {
                return new AvailabilityResponseDTO
                {
                    IsAvailable = false,
                    Message = validation.ErrorMessage
                };
            }

            var availableTableId = await _bookingRepository.FindFirstAvailableTableAsync(
                availabilityRequest.BookingDate,
                availabilityRequest.NumberOfGuests);

            var allAvailableTables = await _bookingRepository.GetAllAvailableTablesAsync(
                availabilityRequest.BookingDate,
                availabilityRequest.NumberOfGuests);

            if (availableTableId.HasValue)
            {
                var table = await _tableRepository.GetTableByIdAsync(availableTableId.Value);
                return new AvailabilityResponseDTO
                {
                    IsAvailable = true,
                    AvailableTableId = availableTableId,
                    TableNumber = table?.TableNumber,
                    AllAvailableTableIds = allAvailableTables.ToList(),
                    Message = $"Table {table?.TableNumber} is available for {availabilityRequest.NumberOfGuests} guests at {availabilityRequest.BookingDate:yyyy-MM-dd HH:mm}."
                };
            }

            return new AvailabilityResponseDTO
            {
                IsAvailable = false,
                AllAvailableTableIds = allAvailableTables.ToList(),
                Message = $"No tables available for {availabilityRequest.NumberOfGuests} guests at {availabilityRequest.BookingDate:yyyy-MM-dd HH:mm}."
            };
        }
        public async Task<IEnumerable<int>> GetAvailableTablesByTimeAsync(DateTime requestedTime, int partySize)
        {
            return await _bookingRepository.GetAllAvailableTablesAsync(requestedTime, partySize);
        }
    }
}
