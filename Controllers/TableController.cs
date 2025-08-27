using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBookingAPI.Services.IServices;
using RestaurantBookingAPI.Mappers;
using RestaurantBookingAPI.DTOs;

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
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult<TableDTO>> CreateTable([FromBody] CreateTableDTO createTableDto)
        {
            if (createTableDto == null)
            {
                return BadRequest("Table cannot be null");
            }

            try
            {
                var createdTable = await _tableService.CreateTableAsync(createTableDto);
                return CreatedAtAction(nameof(GetTableById), new { id = createdTable.Id }, createdTable);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<TableDTO>> UpdateTable([FromBody] UpdateTableDTO updateTableDto)
        {
            if (updateTableDto == null)
            {
                return BadRequest("Table cannot be null");
            }

            try
            {
                var updatedTable = await _tableService.UpdateTableAsync(updateTableDto.TableNumber, updateTableDto);
                return Ok(updatedTable);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTable(int id)
        {
            try
            {
                var result = await _tableService.DeleteTableAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("availability")]
        public async Task<ActionResult<AvailabilityResponseDTO>> CheckAvailability([FromBody] AvailabilityRequestDTO availabilityRequest)
        {
            if (availabilityRequest == null)
            {
                return BadRequest("Availability request cannot be null");
            }

            var result = await _tableService.GetTableAvailabilityAsync(availabilityRequest);
            return Ok(result);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAvailableTables()
        {
            var tables = await _tableService.GetAvailableTablesAsync();
            return Ok(tables);
        }

        [HttpGet("capacity/{minCapacity}")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetTablesByCapacity(int minCapacity)
        {
            var tables = await _tableService.GetTablesByCapacityAsync(minCapacity);
            return Ok(tables);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<TableSummaryDTO>>> GetTablesSummary()
        {
            var summary = await _tableService.GetTablesSummaryAsync();
            return Ok(summary);
        }

        [HttpPatch("{id}/availability")]
        public async Task<ActionResult<bool>> SetTableAvailability(int id, [FromBody] SetTableAvailabilityDTO availabilityDto)
        {
            if (availabilityDto == null)
            {
                return BadRequest("Availability data cannot be null");
            }

            try
            {
                var result = await _tableService.SetTableAvailabilityAsync(id, availabilityDto.IsAvailable);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("number/{tableNumber}")]
        public async Task<ActionResult<TableDTO>> GetTableByNumber(int tableNumber)
        {
            var table = await _tableService.GetTableByNumberAsync(tableNumber);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }
    }
}
