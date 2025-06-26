using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using Hien_mau.Dtos;
using Hien_mau.Models;
using System.Threading.Tasks;

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
            var inventories = await _context.BloodInventory
                .AsNoTracking()
                .Select(i => new BloodInventoryDto
                {
                    BloodGroup = i.BloodGroup,
                    RhType = i.RhType,
                    ComponentType = i.ComponentType,
                    Quantity = i.Quantity,
                    Status = i.Status,
                    IsRare = i.IsRare ?? false,
                    LastUpdated = i.LastUpdated ?? DateTime.Now,
                    ExpirationDate = i.ExpirationDate
                })
                .ToListAsync();
            return Ok(inventories);
        }

        [HttpGet("history/all")]
        public async Task<IActionResult> GetAllBloodInventoryHistory()
        {
            try
            {
                var histories = await _context.BloodInventoryHistory
                    .AsNoTracking()
                    .Select(h => new
                    {
                        h.PerformedAt,
                        h.BloodGroup,
                        h.RhType,
                        h.ComponentType,
                        h.Quantity,
                        h.ActionType,
                        h.BagType,
                        h.Notes,
                        h.ExpirationDate,
                        h.PerformedBy
                    })
                    .OrderByDescending(h => h.PerformedAt)
                    .ToListAsync();

                var userIds = histories.Select(h => h.PerformedBy).Distinct().ToList();
                var users = await _context.Users
                    .AsNoTracking()
                    .Where(u => userIds.Contains(u.UserId))
                    .ToDictionaryAsync(u => u.UserId, u => u.Name ?? "Unknown");

                var result = histories.Select(h => new BloodHistoryDto
                {
                    PerformedAt = h.PerformedAt,
                    BloodGroup = h.BloodGroup ?? "",
                    RhType = h.RhType ?? "",
                    ComponentType = h.ComponentType ?? "",
                    Quantity = h.Quantity,
                    PerformedByName = users.TryGetValue(h.PerformedBy, out var name) ? name : "System",
                    ActionType = h.ActionType ?? "",
                    BagType = h.BagType,
                    Notes = h.Notes,
                    ExpirationDate = h.ExpirationDate
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("check-in")]
        public async Task<IActionResult> CheckInBlood([FromBody] CheckInOutRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (request.Quantity <= 0)
                    return BadRequest("Số lượng phải lớn hơn 0.");

                if (!IsValidBloodGroup(request.BloodGroup) || !IsValidRhType(request.RhType) ||
                    !IsValidComponentType(request.ComponentType) || !IsValidBagType(request.BagType))
                    return BadRequest("Thông tin máu không hợp lệ.");

                var userExists = await _context.Users.AsNoTracking().AnyAsync(u => u.UserId == request.PerformedBy);
                if (!userExists)
                    return BadRequest("Người thực hiện không tồn tại.");

                var inventory = await _context.BloodInventory
                    .AsNoTracking()
                    .FirstOrDefaultAsync(i => i.BloodGroup == request.BloodGroup &&
                                            i.RhType == request.RhType &&
                                            i.ComponentType == request.ComponentType &&
                                            i.BagType == request.BagType);

                int mlQuantity = request.Quantity * GetBagSize(request.BagType);
                var receivedDate = DateTime.Now;
                int? inventoryId;

                // Tính ExpirationDate
                DateTime? expirationDate = request.ComponentType switch
                {
                    "Toàn phần" => receivedDate.AddDays(35),
                    "Hồng cầu" => receivedDate.AddDays(42),
                    "Tiểu cầu" => receivedDate.AddDays(5),
                    "Huyết tương" => receivedDate.AddDays(365),
                    _ => null
                };

                if (inventory == null)
                {
                    var newInventory = new BloodInventory
                    {
                        BloodGroup = request.BloodGroup,
                        RhType = request.RhType,
                        ComponentType = request.ComponentType,
                        BagType = request.BagType,
                        Quantity = mlQuantity,
                        Status = UpdateStatus(mlQuantity),
                        IsRare = IsRareBloodType(request.BloodGroup, request.RhType),
                        ReceivedDate = receivedDate,
                        LastUpdated = DateTime.Now,
                        ExpirationDate = expirationDate
                    };
                    _context.BloodInventory.Add(newInventory);
                    await _context.SaveChangesAsync();

                    inventoryId = await _context.BloodInventory
                        .AsNoTracking()
                        .Where(i => i.BloodGroup == request.BloodGroup &&
                                    i.RhType == request.RhType &&
                                    i.ComponentType == request.ComponentType &&
                                    i.BagType == request.BagType)
                        .Select(i => i.InventoryID)
                        .FirstOrDefaultAsync();
                }
                else
                {
                    inventoryId = inventory.InventoryID;
                    var updatedInventory = new BloodInventory
                    {
                        InventoryID = inventoryId.Value,
                        BloodGroup = inventory.BloodGroup,
                        RhType = inventory.RhType,
                        ComponentType = inventory.ComponentType,
                        BagType = inventory.BagType,
                        Quantity = inventory.Quantity + mlQuantity,
                        Status = UpdateStatus(inventory.Quantity + mlQuantity),
                        IsRare = inventory.IsRare,
                        ReceivedDate = receivedDate,
                        LastUpdated = DateTime.Now,
                        ExpirationDate = expirationDate
                    };
                    _context.BloodInventory.Update(updatedInventory);
                    await _context.SaveChangesAsync();
                }

                if (inventoryId == null)
                    throw new Exception("Không tìm thấy InventoryID.");

                var history = new BloodInventoryHistory
                {
                    InventoryID = inventoryId.Value,
                    BloodGroup = request.BloodGroup,
                    RhType = request.RhType,
                    ComponentType = request.ComponentType,
                    ActionType = "Xuất",
                    Quantity = request.Quantity,
                    BagType = request.BagType,
                    Notes = request.Notes,
                    PerformedBy = request.PerformedBy,
                    PerformedAt = DateTime.Now,
                    ReceivedDate = receivedDate,
                    ExpirationDate = expirationDate
                };

                _context.BloodInventoryHistory.Add(history);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { Message = "Nhập kho thành công" });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOutBlood([FromBody] CheckInOutRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (request.Quantity <= 0)
                    return BadRequest("Số lượng phải lớn hơn 0.");

                if (!IsValidBloodGroup(request.BloodGroup) || !IsValidRhType(request.RhType) ||
                    !IsValidComponentType(request.ComponentType) || !IsValidBagType(request.BagType))
                    return BadRequest("Thông tin máu không hợp lệ.");

                var userExists = await _context.Users.AsNoTracking().AnyAsync(u => u.UserId == request.PerformedBy);
                if (!userExists)
                    return BadRequest("Người thực hiện không tồn tại.");

                var inventory = await _context.BloodInventory
                    .AsNoTracking()
                    .FirstOrDefaultAsync(i => i.BloodGroup == request.BloodGroup &&
                                            i.RhType == request.RhType &&
                                            i.ComponentType == request.ComponentType &&
                                            i.BagType == request.BagType);

                if (inventory == null)
                    return NotFound("Không tìm thấy kho máu.");

                if (inventory.ExpirationDate < DateTime.Now)
                    return BadRequest("Máu đã hết hạn sử dụng.");

                int mlQuantity = request.Quantity * GetBagSize(request.BagType);
                if (inventory.Quantity < mlQuantity)
                    return BadRequest("Số lượng trong kho không đủ.");

                // Tính lại ExpirationDate
                DateTime? expirationDate = request.ComponentType switch
                {
                    "Toàn phần" => inventory.ReceivedDate.AddDays(35),
                    "Hồng cầu" => inventory.ReceivedDate.AddDays(42),
                    "Tiểu cầu" => inventory.ReceivedDate.AddDays(5),
                    "Huyết tương" => inventory.ReceivedDate.AddDays(365),
                    _ => inventory.ExpirationDate
                };

                var updatedInventory = new BloodInventory
                {
                    InventoryID = inventory.InventoryID,
                    BloodGroup = inventory.BloodGroup,
                    RhType = inventory.RhType,
                    ComponentType = inventory.ComponentType,
                    BagType = inventory.BagType,
                    Quantity = inventory.Quantity - mlQuantity,
                    IsRare = inventory.IsRare,
                    Status = UpdateStatus(inventory.Quantity - mlQuantity),
                    ReceivedDate = inventory.ReceivedDate,
                    LastUpdated = DateTime.Now,
                    ExpirationDate = expirationDate
                };

                _context.BloodInventory.Update(updatedInventory);
                await _context.SaveChangesAsync();

                var inventoryId = inventory.InventoryID;

                var history = new BloodInventoryHistory
                {
                    InventoryID = inventoryId,
                    BloodGroup = inventory.BloodGroup,
                    RhType = inventory.RhType,
                    ComponentType = inventory.ComponentType,
                    ActionType = "Thêm",
                    Quantity = request.Quantity,
                    BagType = inventory.BagType,
                    Notes = request.Notes,
                    PerformedBy = request.PerformedBy,
                    PerformedAt = DateTime.Now,
                    ReceivedDate = inventory.ReceivedDate,
                    ExpirationDate = expirationDate
                };

                _context.BloodInventoryHistory.Add(history);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { Message = "Xuất kho thành công" });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpPost("cancel-expired")]
        public async Task<IActionResult> CancelExpiredBlood()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var expiredInventories = await _context.BloodInventory
                    .AsNoTracking()
                    .Where(i => i.ExpirationDate < DateTime.Now && i.Quantity > 0)
                    .ToListAsync();

                foreach (var inventory in expiredInventories)
                {
                    var updatedInventory = new BloodInventory
                    {
                        InventoryID = inventory.InventoryID,
                        BloodGroup = inventory.BloodGroup,
                        RhType = inventory.RhType,
                        ComponentType = inventory.ComponentType,
                        BagType = inventory.BagType,
                        Quantity = 0,
                        IsRare = inventory.IsRare,
                        Status = 0,
                        ReceivedDate = inventory.ReceivedDate,
                        LastUpdated = DateTime.Now,
                        ExpirationDate = inventory.ExpirationDate
                    };

                    _context.BloodInventory.Update(updatedInventory);

                    var history = new BloodInventoryHistory
                    {
                        InventoryID = inventory.InventoryID,
                        BloodGroup = inventory.BloodGroup,
                        RhType = inventory.RhType,
                        ComponentType = inventory.ComponentType,
                        ActionType = "Hủy",
                        Quantity = inventory.Quantity / GetBagSize(inventory.BagType),
                        BagType = inventory.BagType,
                        Notes = "Máu hết hạn tự động hủy",
                        PerformedBy = 1,
                        PerformedAt = DateTime.Now,
                        ReceivedDate = inventory.ReceivedDate,
                        ExpirationDate = inventory.ExpirationDate
                    };

                    _context.BloodInventoryHistory.Add(history);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return Ok(new { Message = "Hủy máu hết hạn thành công" });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private bool IsValidBloodGroup(string bloodGroup) =>
            new[] { "A", "B", "AB", "O" }.Contains(bloodGroup);

        private bool IsValidRhType(string rhType) =>
            new[] { "Rh+", "Rh-" }.Contains(rhType);

        private bool IsValidComponentType(string componentType) =>
            new[] { "Toàn phần", "Hồng cầu", "Huyết tương", "Tiểu cầu" }.Contains(componentType);

        private bool IsValidBagType(string bagType) =>
            new[] { "250ml", "350ml", "450ml" }.Contains(bagType);

        private bool IsRareBloodType(string bloodGroup, string rhType) =>
            (bloodGroup == "AB" && rhType == "-") || (bloodGroup == "O" && rhType == "-") ||
            (bloodGroup == "A" && rhType == "-") || (bloodGroup == "B" && rhType == "-");

        private byte UpdateStatus(int quantity) =>
            quantity switch
            {
                <= 2500 => 0, // Khẩn cấp
                <= 7500 => 1, // Thiếu
                <= 15000 => 2, // Trung bình
                _ => 3        // An toàn
            };

        private int GetBagSize(string bagType) =>
            bagType switch
            {
                "250ml" => 250,
                "350ml" => 350,
                "450ml" => 450,
                _ => 0
            };
    }
}