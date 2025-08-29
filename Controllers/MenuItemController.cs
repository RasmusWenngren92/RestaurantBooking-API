using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemController(IMenuItemService menuItemService, IConfiguration configuration)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet("GetAllMenuItems")]
        public async Task<ActionResult<List<MenuItemDTO>>> GetAllMenuItems()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }
        [HttpGet("GetMenuItemById/{menuId}")]
        public async Task<ActionResult<MenuItemDTO>> GetMenuItemById(int menuId)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(menuId);
 
            return Ok(menuItem);
        }
        [Authorize]
        [HttpPost("AddMenuItem")]
        public async Task<ActionResult<int>> AddMenuItem(MenuItemDTO menuItemDTO)
        {

            var result = await _menuItemService.AddMenuItemAsync(menuItemDTO);
            return Ok(true);
        }
        [Authorize]
        [HttpPut("UpdateMenuItem")]
        public async Task<ActionResult<bool>> UpdateMenuItem(MenuItemDTO menuItemDTO)
        {

            var result = await _menuItemService.UpdateMenuItemAsync(menuItemDTO);

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("DeleteMenuItem/{menuItemId}")]
        public async Task<ActionResult<bool>> DeleteMenuItem(int menuItemId)
        {
            var result = await _menuItemService.DeleteMenuItemAsync(menuItemId);

            return Ok(result);
        }
    }

}
