using RestaurantBookingAPI.DTOs;

namespace RestaurantBookingAPI.Services.IServices
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemDTO>> GetAllMenuItemsAsync();
        Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuId);
        Task<int> AddMenuItemAsync(MenuItemDTO menuItemDTO);
        Task<bool> UpdateMenuItemAsync(MenuItemDTO menuItemDTO);
        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}
