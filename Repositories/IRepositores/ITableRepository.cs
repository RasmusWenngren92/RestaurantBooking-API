


using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Repositories.IRepositores
{
    public interface ITableRepository
    {
       
       
        Task<Table?> GetTableByIdAsync(int id);
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> CreateTableAsync(Table table);
        Task<Table> UpdateTableAsync(Table table);
        Task<bool> DeleteTableAsync(int id);

       
        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<IEnumerable<Table>> GetTablesByCapacityAsync(int minCapacity);
        Task<Table?> GetTableByNumberAsync(int tableNumber);
        Task<bool> TableNumberExistsAsync(int tableNumber);

       
        Task<bool> SetTableAvailabilityAsync(int tableId, bool isAvailable);
        Task<IEnumerable<Table>> GetTablesSuitableForPartySizeAsync(int partySize);
    }

}
