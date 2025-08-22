using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using RestauantBookingAPI.Data;
using RestauantBookingAPI.Models.Entities;
using RestauantBookingAPI.Repositories.IRepositores;

namespace RestauantBookingAPI.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
      private readonly RestaurantDBContext _context;
        public MenuItemRepository(RestaurantDBContext context)
        {
            _context = context;
        }

        public async Task<int> AddMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem.Id;
        }
        public async Task<bool> DeleteMenuItemAsync(int menuItemId)
        {
            var rowsAffected = await _context.MenuItems
                .Where(m => m.Id == menuItemId)
                .ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                               return true;
            }
            return false;
        }
        public async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            var menuItems = await _context.MenuItems
                .AsNoTracking()
                .ToListAsync();
            return menuItems;
        }
        public async Task<MenuItem> GetMenuItemByIdAsync(int menuId)
        {
            var menuItem = await _context.MenuItems
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == menuId);
            return menuItem;
        }
        public async Task<bool> UpdateMenuItemAsync(MenuItem menuItem)
        {
           _context.MenuItems.Update(menuItem);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }
    }
}
