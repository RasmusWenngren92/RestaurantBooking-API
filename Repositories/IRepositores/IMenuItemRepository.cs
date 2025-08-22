using RestauantBookingAPI.Models.Entities;

namespace RestauantBookingAPI.Repositories.IRepositores
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(int menuId);
        Task<int> AddMenuItemAsync(MenuItem menuItem);
        Task<bool> UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}
