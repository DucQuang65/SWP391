using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using Hien_mau.Dtos;
using System.Threading.Tasks;
using Hien_mau.Models;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodInventoryController : ControllerBase
    {
        private readonly Hien_mauContext _context;

        public BloodInventoryController(Hien_mauContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBloodInventories()
        {
            var inventories = await _context.BloodInventories
                .Select(i => new
                {
                    i.InventoryId,
                    i.BloodGroup,
                    i.RhType,
                    i.ComponentType,
                    i.Quantity,
                    i.IsRare,
                    i.Status,
                    i.LastUpdated
                })
                .ToListAsync();
            return Ok(inventories);
        }

        [HttpPost("check-in")]
        public async Task<IActionResult> CheckInBlood([FromBody] CheckInOutRequestDto request)
        {
            if (request.Quantity <= 0)
                return BadRequest("Quantity must be positive.");

            var inventory = await _context.BloodInventories
                .FirstOrDefaultAsync(i => i.InventoryId == request.InventoryId);
            if (inventory == null)
                return NotFound("Inventory not found.");

            inventory.Quantity += request.Quantity;
            inventory.LastUpdated = DateTime.Now;
            inventory.Status = UpdateStatus(inventory.Quantity);

            var history = new BloodInventoryHistory
            {
                InventoryId = request.InventoryId,
                BloodGroup = inventory.BloodGroup,
                RhType = inventory.RhType,
                ComponentType = inventory.ComponentType,
                ActionType = "CheckIn",
                Quantity = request.Quantity,
                Reason = request.Reason,
                Notes = request.Notes,
                PerformedBy = request.PerformedBy
            };
            _context.BloodInventoryHistories.Add(history);

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Check-in successful" });
        }

        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOutBlood([FromBody] CheckInOutRequestDto request)
        {
            if (request.Quantity <= 0)
                return BadRequest("Quantity must be positive.");

            var inventory = await _context.BloodInventories
                .FirstOrDefaultAsync(i => i.InventoryId == request.InventoryId);
            if (inventory == null)
                return NotFound("Inventory not found.");

            if (inventory.Quantity < request.Quantity)
                return BadRequest("Insufficient quantity in inventory.");

            inventory.Quantity -= request.Quantity;
            inventory.LastUpdated = DateTime.Now;
            inventory.Status = UpdateStatus(inventory.Quantity);

            var history = new BloodInventoryHistory
            {
                InventoryId = request.InventoryId,
                BloodGroup = inventory.BloodGroup,
                RhType = inventory.RhType,
                ComponentType = inventory.ComponentType,
                ActionType = "CheckOut",
                Quantity = request.Quantity,
                Reason = request.Reason,
                Notes = request.Notes,
                PerformedBy = request.PerformedBy
            };
            _context.BloodInventoryHistories.Add(history);

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Check-out successful" });
        }

        [HttpGet("history/all")]
        public async Task<IActionResult> GetAllBloodInventoryHistory()
        {
            var histories = await _context.BloodInventoryHistories
                .Include(h => h.PerformedByUser)
                .Select(h => new
                {
                    h.HistoryId,
                    h.InventoryId,
                    h.BloodGroup,
                    h.RhType,
                    h.ComponentType,
                    h.ActionType,
                    h.Quantity,
                    h.Reason,
                    h.Notes,
                    PerformedBy = new
                    {
                        h.PerformedByUser.UserId,
                        h.PerformedByUser.Name
                    },
                    h.PerformedAt
                })
                .OrderByDescending(h => h.PerformedAt)
                .ToListAsync();

            return Ok(histories);
        }

        private byte UpdateStatus(int quantity)
        {
            if (quantity <= 10) return 0; 
            if (quantity <= 30) return 1; 
            if (quantity <= 60) return 2; 
            return 3; 
        }
    }
}