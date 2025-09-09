using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Mappers;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Repositories.IRepositores;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly RestaurantDBContext _context;
        private readonly IMenuItemRepository _menuItemRepository;
        
        public MenuItemService(IMenuItemRepository menuItemRepository, RestaurantDBContext context)
        {
            _menuItemRepository = menuItemRepository;
            _context = context;
        }
        public async Task<IEnumerable<MenuItemDTO>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItemsAsync();
            return menuItems.Select(DomainMapper.ToMenuItemDTO);

        }

        public async Task<MenuItemDTO?> GetMenuItemByIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            return menuItem == null ? null : DomainMapper.ToMenuItemDTO(menuItem);
        }
        public async Task<int> AddMenuItemAsync(MenuItemDTO menuItemDTO)
        {
            if (menuItemDTO == null)
                throw new ArgumentException("Menu item data is required");

            if (string.IsNullOrEmpty(menuItemDTO.Name))
                throw new ArgumentException("Menu item name is required");

            if (string.IsNullOrEmpty(menuItemDTO.Category))
                throw new ArgumentException("Menu item category is required");

            if (menuItemDTO.Price <= 0)
                throw new ArgumentException("Menu item price must be greater than zero");
            var menuItem = DomainMapper.ToMenuItem(menuItemDTO);
            return await _menuItemRepository.AddMenuItemAsync(menuItem);
        }
        public async Task<bool> UpdateMenuItemAsync(int id, MenuItemDTO menuItemDTO)
        {
            ArgumentNullException.ThrowIfNull(menuItemDTO);
            ArgumentException.ThrowIfNullOrEmpty(menuItemDTO.Name);
            ArgumentException.ThrowIfNullOrEmpty(menuItemDTO.Category);
            var rowsAffected = await _context.MenuItems
                .Where(m => m.Id == menuItemDTO.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Name, menuItemDTO.Name)
                    .SetProperty(m => m.Description, menuItemDTO.Description)
                    .SetProperty(m => m.Price, menuItemDTO.Price)
                    .SetProperty(m => m.Category, menuItemDTO.Category)
                    .SetProperty(m => m.IsPopular, menuItemDTO.IsPopular)
                    .SetProperty(m => m.ImageUrl, menuItemDTO.ImageUrl ?? string.Empty));

            return rowsAffected > 0;
        }
        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            return await _menuItemRepository.DeleteMenuItemAsync(menuItemId);
        }

    }
}
