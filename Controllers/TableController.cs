using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Mappers;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {

        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTables() =>
            Ok(await _tableService.GetAllTablesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TableDTO>> GetTableById(int id) =>
            Ok(await _tableService.GetTableByIdAsync(id));

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TableDTO>> CreateTable([FromBody] CreateTableDTO dto)
        {
            var created = await _tableService.CreateTableAsync(dto);
            return CreatedAtAction(nameof(GetTableById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{tableNumber}")]
        public async Task<ActionResult<TableDTO>> UpdateTable(int tableNumber, [FromBody] UpdateTableDTO dto) =>
            Ok(await _tableService.UpdateTableAsync(tableNumber, dto));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTable(int id) =>
            Ok(await _tableService.DeleteTableAsync(id));

        [HttpPost("check-availability")]
        public async Task<ActionResult<AvailabilityResponseDTO>> CheckAvailability([FromBody] AvailabilityRequestDTO dto) =>
            Ok(await _tableService.GetTableAvailabilityAsync(dto));

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAvailableTables() =>
            Ok(await _tableService.GetAvailableTablesAsync());

        [HttpGet("capacity/{minCapacity}")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetTablesByCapacity(int minCapacity) =>
            Ok(await _tableService.GetTablesByCapacityAsync(minCapacity));

        [Authorize]
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<TableSummaryDTO>>> GetTablesSummary() =>
            Ok(await _tableService.GetTablesSummaryAsync());

        [HttpPatch("{id}/availability")]
        public async Task<ActionResult<bool>> SetTableAvailability(int id, [FromBody] SetTableAvailabilityDTO dto) =>
            Ok(await _tableService.SetTableAvailabilityAsync(id, dto.IsAvailable));

        [HttpGet("number/{tableNumber}")]
        public async Task<ActionResult<TableDTO>> GetTableByNumber(int tableNumber) =>
            Ok(await _tableService.GetTableByNumberAsync(tableNumber));

        [Authorize]
        [HttpGet("{id}/with-booking-count")]
        public async Task<ActionResult<TableDTO>> GetTableWithBookingCount(int id)
        {
            var tableWithCount = await _tableService.GetTableWithBookingCountAsync(id);
            if (tableWithCount == null) return NotFound();
            return Ok(tableWithCount);
        }
    }
}
