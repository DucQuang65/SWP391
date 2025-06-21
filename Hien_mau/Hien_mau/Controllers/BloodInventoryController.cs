using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Data;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

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
    }
}