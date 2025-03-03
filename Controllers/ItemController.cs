using CampaignNexus.Data;
using CampaignNexus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public ItemController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    [HttpPost("toggle/{id}")]
    [Authorize]
    public IActionResult ItemEquip(int id)
    {
        try
        {
            //Find the character item entity
            CharacterItem item = _dbContext
            .CharacterItems
            .SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound(new {message = "This Character does not have this item"});
            }

            //Set the IsEquipped to the opposite 
            item.IsEquipped = !item.IsEquipped;

            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in ItemEquip {ex}");
            return StatusCode(500, "There was an error equipping/unequipping this item");
        }
    }
}