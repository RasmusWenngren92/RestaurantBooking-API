using RestaurantBookingAPI.Services.IServices;
using RestaurantBookingAPI.Repositories.IRepositores;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;

namespace RestaurantBookingAPI.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository; 
        }
        public async Task<IEnumerable<MenuItemDTO>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItemsAsync();
            
            var menuItemDTO = menuItems.Select(m => new MenuItemDTO
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Category = m.Category,
                IsPopular = m.IsPopular,
                ImageUrl = m.ImageUrl
            }).ToList();

            return menuItemDTO;
        }

        public async Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null)
            {
                return null;
            }
            return new MenuItemDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Category = menuItem.Category,
                IsPopular = menuItem.IsPopular,
                ImageUrl = menuItem.ImageUrl
            };
        }
        public async Task<int> AddMenuItemAsync(MenuItemDTO menuItemDTO)
        {
            ArgumentNullException.ThrowIfNull(menuItemDTO);
            ArgumentException.ThrowIfNullOrEmpty(menuItemDTO.Name);
            ArgumentException.ThrowIfNullOrEmpty(menuItemDTO.Category);
            var menuItem = new MenuItem
            {
                Name = menuItemDTO.Name,
                Description = menuItemDTO.Description,
                Price = menuItemDTO.Price,
                Category = menuItemDTO.Category,
                IsPopular = menuItemDTO.IsPopular,
                ImageUrl = menuItemDTO.ImageUrl ?? string.Empty
            };
            return await _menuItemRepository.AddMenuItemAsync(menuItem);
        }
        public async Task<bool> UpdateMenuItemAsync(MenuItemDTO menuItemDTO)
        {
            ArgumentNullException.ThrowIfNull(menuItemDTO);
            ArgumentException.ThrowIfNullOrEmpty(menuItemDTO.Name);
            ArgumentException.ThrowIfNullOrEmpty(menuItemDTO.Category);
            var menuItem = new MenuItem
            {
                Id = menuItemDTO.Id,
                Name = menuItemDTO.Name,
                Description = menuItemDTO.Description,
                Price = menuItemDTO.Price,
                Category = menuItemDTO.Category,
                IsPopular = menuItemDTO.IsPopular,
                ImageUrl = menuItemDTO.ImageUrl ?? string.Empty
            };
            return await _menuItemRepository.UpdateMenuItemAsync(menuItem);
        }
        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            return await _menuItemRepository.DeleteMenuItemAsync(menuItemId);
        }

    }
}
