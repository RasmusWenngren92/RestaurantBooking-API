using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Repositories.IRepositores
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem?> GetMenuItemByIdAsync(int menuId);
        Task<int> AddMenuItemAsync(MenuItem menuItem);
        Task<bool> UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}
