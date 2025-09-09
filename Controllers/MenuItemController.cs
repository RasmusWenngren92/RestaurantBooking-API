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

        [HttpGet]
        public async Task<ActionResult<List<MenuItemDTO>>> GetAllMenuItems() =>
        Ok(await _menuItemService.GetAllMenuItemsAsync());

        [HttpGet("{menuItemId}")]
        public async Task<ActionResult<MenuItemDTO>> GetMenuItemById(int menuItemId) =>
        Ok(await _menuItemService.GetMenuItemByIdAsync(menuItemId));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> AddMenuItem([FromBody] MenuItemDTO dto)
        {
            var newId = await _menuItemService.AddMenuItemAsync(dto);
            return CreatedAtAction(nameof(GetMenuItemById), new { menuItemId = newId }, newId);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateMenuItem(int id,MenuItemDTO menuItemDTO)
        {

            var result = await _menuItemService.UpdateMenuItemAsync(id, menuItemDTO);

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{menuItemId}")]
        public async Task<ActionResult<bool>> DeleteMenuItem(int menuItemId) =>
        Ok(await _menuItemService.DeleteMenuItemAsync(menuItemId));
    }

}
