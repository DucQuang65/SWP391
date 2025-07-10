using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_mau.Models;
using Hien_mau.Data;

namespace Hien_mau.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly Hien_mauContext _context;

    public TagsController(Hien_mauContext context)
    {
        _context = context;
    }

   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tags>>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }


    [HttpPost]
    public async Task<ActionResult<Tags>> CreateTag([FromBody] TagDto tagDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var tag = new Tags
        {
            TagName = tagDto.TagName
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return Ok(tag); 
    }
}
