using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface ITableService
    {
        Task<TableDTO?> GetTableByIdAsync(int id);
        Task<IEnumerable<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> CreateTableAsync(CreateTableDTO createTableDto);
        Task<TableDTO> UpdateTableAsync(int id, UpdateTableDTO updateTableDto);
        Task<bool> DeleteTableAsync(int id);

       
        Task<IEnumerable<TableSummaryDTO>> GetTablesSummaryAsync();
        Task<IEnumerable<TableDTO>> GetAvailableTablesAsync();
        Task<IEnumerable<TableDTO>> GetTablesByCapacityAsync(int minCapacity);
        Task<TableDTO?> GetTableByNumberAsync(int tableNumber);

        
        Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable);
        Task<bool> TableNumberExistsAsync(int tableNumber);

        
        Task<IEnumerable<TableDTO>> GetTablesSuitableForPartySizeAsync(int partySize);
        Task<string> GetTableStatusAsync(int tableId);
    }
}
