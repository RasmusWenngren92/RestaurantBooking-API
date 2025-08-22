using RestauantBookingAPI.DTOs;

namespace RestauantBookingAPI.Services.IServices
{
    public interface IMenuItemService
    {
        Task<List<MenuItemDTO>> GetAllMenuItemsAsync();
        Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuId);
        Task<int> AddMenuItemAsync(MenuItemDTO menuItemDTO);
        Task<bool> UpdateMenuItemAsync(MenuItemDTO menuItemDTO);
        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}
