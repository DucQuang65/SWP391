using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using Hien_mau.Dtos;
using Hien_mau.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Hien_mau.Dtos.BloodInventoryDtos;
using Microsoft.Extensions.Logging;

namespace Hien_mau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodInventoryController : ControllerBase
    {
        private readonly Hien_mauContext _context;
        private readonly ILogger<BloodInventoryController> _logger;

       
        private static readonly string[] ValidBloodGroups = { "A", "B", "AB", "O" };
        private static readonly string[] ValidRhTypes = { "Rh+", "Rh-" };
        private static readonly string[] ValidBagTypes = { "250ml", "350ml", "450ml" };

        public BloodInventoryController(Hien_mauContext context, ILogger<BloodInventoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBloodInventories()
        {
            try
            {
                var inventories = await _context.BloodInventories
                    .AsNoTracking()
                    .Select(i => new BloodInventoryDto
                    {
                        InventoryID = i.InventoryId,
                        BloodGroup = i.BloodGroup ?? "",
                        RhType = i.RhType ?? "",
                        ComponentId = i.ComponentId,
                        BagType = i.BagType ?? "",
                        Quantity = i.Quantity,
                        Status = i.Quantity <= 10 ? (byte)0 :
                                 i.Quantity <= 30 ? (byte)1 :
                                 i.Quantity <= 60 ? (byte)2 : (byte)3,
                        IsRare = i.IsRare,
                        LastUpdated = i.LastUpdated
                    })
                    .ToListAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blood inventories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("history/all")]
        public async Task<IActionResult> GetAllBloodInventoryHistory()
        {
            try
            {
                var histories = await _context.BloodInventoryHistories
                    .AsNoTracking()
                    .Include(h => h.Inventory)
                    .Select(h => new
                    {
                        h.InventoryId,
                        h.PerformedAt,
                        BloodGroup = h.Inventory != null ? h.Inventory.BloodGroup : "",
                        RhType = h.Inventory != null ? h.Inventory.RhType : "",
                        ComponentId = h.Inventory != null ? h.Inventory.ComponentId : 0,
                        h.Quantity,
                        h.ActionType,
                        h.BagType,
                        h.Notes,
                        h.ReceivedDate,
                        h.ExpirationDate,
                        h.PerformedBy
                    })
                    .OrderByDescending(h => h.PerformedAt)
                    .ToListAsync();

                var userIds = histories.Select(h => h.PerformedBy).Where(id => id != -1).Distinct().ToList();
                var users = await _context.Users
                    .AsNoTracking()
                    .Where(u => userIds.Contains(u.UserId))
                    .ToDictionaryAsync(u => u.UserId, u => u.Name ?? "Unknown");

                var result = histories.Select(h => new BloodHistoryDto
                {
                    InventoryID = h.InventoryId,
                    PerformedAt = h.PerformedAt,
                    BloodGroup = h.BloodGroup ?? "",
                    RhType = h.RhType ?? "",
                    
                    Quantity = h.Quantity,
                    PerformedByName = h.PerformedBy == -1 ? "Hệ thống" : users.TryGetValue(h.PerformedBy, out var name) ? name : "Unknown",
                    ActionType = h.ActionType ?? "",
                    BagType = h.BagType ?? "",
                    Notes = h.Notes ?? "",
                    ReceivedDate = h.ReceivedDate,
                    ExpirationDate = h.ExpirationDate
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving blood inventory history");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("selection-options")]
        public async Task<IActionResult> GetSelectionOptions()
        {
            try
            {
                var components = await _context.Components
                    .AsNoTracking()
                    .Select(c => new ComponentSelectionDto
                    {
                        ComponentId = c.ComponentId,
                        ComponentName = c.ComponentType ?? ""
                    })
                    .ToListAsync();

                var selection = new BloodSelectionDto
                {
                    Components = components,
                    BloodGroups = ValidBloodGroups.ToList(),
                    RhTypes = ValidRhTypes.ToList()
                };
                return Ok(selection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving selection options");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetBloodInventoryStatistics()
        {
                var inventoryStats = await _context.BloodInventories
                    .AsNoTracking()
                    .Include(i => i.Components)
                    .GroupBy(i => new
                    {
                        i.BloodGroup,
                        i.RhType,
                        i.ComponentId,
                        i.Components.ComponentType
                    })
                    .Select(g => new BloodInventoryStatisticsDto
                    {
                        BloodGroup = g.Key.BloodGroup,
                        RhType = g.Key.RhType,
                        ComponentId = g.Key.ComponentId,
                        ComponentName = g.Key.ComponentType,
                        Quantity = g.Sum(x => x.Quantity)
                    })
                    .ToListAsync();

                return Ok(inventoryStats);
        }

        [HttpPost("check-in")]
        public async Task<IActionResult> CheckInBlood([FromBody] CheckInOutRequestDto request)
        {
           
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                var validationCheck = await ValidateComponentAndUser(request.ComponentId, request.PerformedBy);
                if (!validationCheck.IsValid)
                    return BadRequest(validationCheck.ErrorMessage);

                var inventory = await _context.BloodInventories
                    .FirstOrDefaultAsync(i => i.BloodGroup == request.BloodGroup &&
                                             i.RhType == request.RhType &&
                                             i.ComponentId == request.ComponentId &&
                                             i.BagType == request.BagType);

                var receivedDate = DateTime.Now;
                var expirationDate = CalculateExpirationDate(request.ComponentId, receivedDate);

                if (inventory == null)
                {
                    inventory = new BloodInventories
                    {
                        BloodGroup = request.BloodGroup,
                        RhType = request.RhType,
                        
                        BagType = request.BagType,
                        Quantity = request.Quantity,
                        IsRare = IsRareBloodType(request.BloodGroup, request.RhType),
                        Status = CalculateStatus(request.Quantity),
                        LastUpdated = DateTime.Now
                    };
                    _context.BloodInventories.Add(inventory);
                }
                else
                {
                    inventory.Quantity += request.Quantity;
                    inventory.Status = CalculateStatus(inventory.Quantity);
                    inventory.LastUpdated = DateTime.Now;

                }

                await _context.SaveChangesAsync();

               
                var history = new BloodInventoryHistories
                {
                    InventoryId = inventory.InventoryId,
                    ActionType = "CheckIn",
                    Quantity = request.Quantity,
                    BagType = request.BagType,
                    Notes = request.Notes,
                    PerformedBy = request.PerformedBy,
                    PerformedAt = DateTime.Now,
                    ReceivedDate = receivedDate,
                    ExpirationDate = expirationDate
                };

                _context.BloodInventoryHistories.Add(history);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogInformation("Blood check-in successful: {BloodGroup} {RhType} {ComponentId} {Quantity}",
                    request.BloodGroup, request.RhType, request.ComponentId, request.Quantity);

                return Ok(new { Message = "Nhập kho thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error during blood check-in");
                return StatusCode(500, "Lỗi hệ thống trong quá trình nhập kho");
            }
        }

        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOutBlood([FromBody] CheckInOutRequestDto request)
        {
         
            var validationResult = ValidateRequest(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                var validationCheck = await ValidateComponentAndUser(request.ComponentId, request.PerformedBy);
                if (!validationCheck.IsValid)
                    return BadRequest(validationCheck.ErrorMessage);

                var inventory = await _context.BloodInventories
                    .FirstOrDefaultAsync(i => i.BloodGroup == request.BloodGroup &&
                                             i.RhType == request.RhType &&
                                             i.ComponentId == request.ComponentId &&
                                             i.BagType == request.BagType);

                if (inventory == null)
                    return NotFound("Không tìm thấy kho máu.");

                if (inventory.Quantity < request.Quantity)
                    return BadRequest("Số lượng không đủ.");

            
                inventory.Quantity -= request.Quantity;
                inventory.Status = CalculateStatus(inventory.Quantity);
                inventory.LastUpdated = DateTime.Now;


                
                var history = new BloodInventoryHistories
                {
                    InventoryId = inventory.InventoryId,
                    ActionType = "CheckOut",
                    Quantity = request.Quantity,
                    BagType = request.BagType,
                    Notes = request.Notes,
                    PerformedBy = request.PerformedBy,
                    PerformedAt = DateTime.Now,
                    ReceivedDate = null, 
                    ExpirationDate = null 
                };

                _context.BloodInventoryHistories.Add(history);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                _logger.LogInformation("Blood check-out successful: {BloodGroup} {RhType} {ComponentId} {Quantity}",
                    request.BloodGroup, request.RhType, request.ComponentId, request.Quantity);

                return Ok(new { Message = "Xuất kho thành công" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error during blood check-out");
                return StatusCode(500, "Lỗi hệ thống trong quá trình xuất kho");
            }
        }

     
        private (bool IsValid, string ErrorMessage) ValidateRequest(CheckInOutRequestDto request)
        {
            if (request == null)
                return (false, "Request không hợp lệ.");

            if (request.Quantity <= 0)
                return (false, "Số lượng phải lớn hơn 0.");

            if (string.IsNullOrEmpty(request.BloodGroup) || !ValidBloodGroups.Contains(request.BloodGroup))
                return (false, "Nhóm máu không hợp lệ.");

            if (string.IsNullOrEmpty(request.RhType) || !ValidRhTypes.Contains(request.RhType))
                return (false, "Loại Rh không hợp lệ.");

            if (string.IsNullOrEmpty(request.BagType) || !ValidBagTypes.Contains(request.BagType))
                return (false, "Túi máu không hợp lệ.");

            return (true, string.Empty);
        }

        private async Task<(bool IsValid, string ErrorMessage)> ValidateComponentAndUser(int componentId, int performedBy)
        {
            var componentExists = await _context.Components.AnyAsync(c => c.ComponentId == componentId);
            if (!componentExists)
                return (false, "Thành phần máu không tồn tại.");

            if (performedBy != -1)
            {
                var userExists = await _context.Users.AnyAsync(u => u.UserId == performedBy);
                if (!userExists)
                    return (false, "Người thực hiện không tồn tại.");
            }

            return (true, string.Empty);
        }

        private DateTime? CalculateExpirationDate(int componentId, DateTime receivedDate)
        {
            return componentId switch
            {
                1 => receivedDate.AddDays(35), 
                2 => receivedDate.AddDays(42), 
                3 => receivedDate.AddDays(365), 
                4 => receivedDate.AddDays(5), 
                _ => null
            };
        }

        private byte CalculateStatus(int quantity) =>
            quantity <= 10 ? (byte)0 :
            quantity <= 30 ? (byte)1 :
            quantity <= 60 ? (byte)2 : (byte)3;

     
        private bool IsRareBloodType(string bloodGroup, string rhType)
        {
            if (string.IsNullOrEmpty(bloodGroup) || string.IsNullOrEmpty(rhType))
                return false;

            
            if (rhType == "Rh-")
            {
                return bloodGroup switch
                {
                    "AB" => true, 
                    "A" => true, 
                    "B" => true, 
                    "O" => false,
                    _ => false
                };
            }
            return false;
        }
    }
}