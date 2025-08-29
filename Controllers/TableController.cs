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

        [HttpGet("GetAllTables")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("GetTableById/{id}")]
        public async Task<ActionResult<TableDTO>> GetTableById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            return Ok(table);
        }
        [Authorize]
        [HttpPost("CreateTable")]
        public async Task<ActionResult<TableDTO>> CreateTable([FromBody] CreateTableDTO createTableDto)
        {
 
                var createdTable = await _tableService.CreateTableAsync(createTableDto);
                return CreatedAtAction(nameof(GetTableById), new { id = createdTable.Id }, createdTable);

        }
        [Authorize]
        [HttpPut("UpdateTable")]
        public async Task<ActionResult<TableDTO>> UpdateTable([FromBody] UpdateTableDTO updateTableDto)
        {
 
                var updatedTable = await _tableService.UpdateTableAsync(updateTableDto.TableNumber, updateTableDto);
                return Ok(updatedTable);
 
        }
        [Authorize]
        [HttpDelete("DeleteTable/{id}")]
        public async Task<ActionResult<bool>> DeleteTable(int id)
        {
    
                var result = await _tableService.DeleteTableAsync(id);
  
                return Ok(result);

        }

        [HttpPost("CheckAvailability")]
        public async Task<ActionResult<AvailabilityResponseDTO>> CheckAvailability([FromBody] AvailabilityRequestDTO availabilityRequest)
        {

            var result = await _tableService.GetTableAvailabilityAsync(availabilityRequest);
            return Ok(result);
        }

        [HttpGet("GetAvailableTables")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAvailableTables()
        {
            var tables = await _tableService.GetAvailableTablesAsync();
            return Ok(tables);
        }

        [HttpGet("GetTablesByCapacity/{minCapacity}")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetTablesByCapacity(int minCapacity)
        {
            var tables = await _tableService.GetTablesByCapacityAsync(minCapacity);
            return Ok(tables);
        }
        [Authorize]
        [HttpGet("GetTableSummary")]
        public async Task<ActionResult<IEnumerable<TableSummaryDTO>>> GetTablesSummary()
        {
            var summary = await _tableService.GetTablesSummaryAsync();
            return Ok(summary);
        }

        [HttpPatch("{id}/SetTableAvailability")]
        public async Task<ActionResult<bool>> SetTableAvailability(int id, [FromBody] SetTableAvailabilityDTO availabilityDto)
        {
 
                var result = await _tableService.SetTableAvailabilityAsync(id, availabilityDto.IsAvailable);

                return Ok(result);

        }

        [HttpGet("GetTableByNumber/{tableNumber}")]
        public async Task<ActionResult<TableDTO>> GetTableByNumber(int tableNumber)
        {
            var table = await _tableService.GetTableByNumberAsync(tableNumber);

            return Ok(table);
        }
        [Authorize]
        [HttpGet("GetTableWithBookingCount/{id}")]
        public async Task<ActionResult<TableDTO>> GetTableWithBookingCount(int id)
        {
            var tableWithCount = await _tableService.GetTableWithBookingCountAsync(id);
            if (tableWithCount == null) return NotFound();

            return Ok(tableWithCount);
        }
    }
}
