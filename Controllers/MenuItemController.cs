using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController(IMenuItemService menuItemService, IConfiguration configuration) : ControllerBase
    {
        private readonly IMenuItemService _menuItemService = menuItemService;
        private readonly IConfiguration _configuration = configuration;

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
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }
        [HttpPost("AddMenuItem")]
        public async Task<ActionResult<int>> AddMenuItem(MenuItemDTO menuItemDTO)
        {
            if (menuItemDTO == null)
            {
                return BadRequest("Menu item cannot be null");
            }
            var menuItemId = await _menuItemService.AddMenuItemAsync(menuItemDTO);
            return CreatedAtAction(nameof(GetMenuItemById), new { menuId = menuItemId }, menuItemId);
        }
        [HttpPut("UpdateMenuItem")]
        public async Task<ActionResult<bool>> UpdateMenuItem(MenuItemDTO menuItemDTO)
        {
            if (menuItemDTO == null)
            {
                return BadRequest("Menu item cannot be null");
            }
            var result = await _menuItemService.UpdateMenuItemAsync(menuItemDTO);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("DeleteMenuItem/{menuItemId}")]
        public async Task<ActionResult<bool>> DeleteMenuItem(int menuItemId)
        {
            var result = await _menuItemService.DeleteMenuItemAsync(menuItemId);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }

}
