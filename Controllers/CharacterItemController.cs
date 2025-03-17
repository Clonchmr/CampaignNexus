using CampaignNexus.Data;
using CampaignNexus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterItemController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public CharacterItemController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    //Uses a charge of a consumable and decreases its quantity by 1.
    //If the consumable chosen is at one charge, it deletes it instead.
    [HttpPost("{id}")]
    public IActionResult UseConsumable(int id)
    {
        try
        {
            CharacterItem itemToUse = _dbContext
            .CharacterItems
            .Include(ci => ci.Item)
            .SingleOrDefault(ci => ci.Id == id);

            if (itemToUse.Item.ItemType != "Consumable")
            {
                return BadRequest("Item must be a consumable");
            }

            if (itemToUse == null)
            {
                return NotFound("That item does not belong to that character");
            }

            if (itemToUse.Quantity > 1) 
            {
                itemToUse.Quantity = itemToUse.Quantity - 1;
                _dbContext.SaveChanges();
                return NoContent();
            } 
            else
            {
                _dbContext.CharacterItems.Remove(itemToUse);
                _dbContext.SaveChanges();
                return NoContent();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in UseConsumable {ex}");
            return StatusCode(500, "An error occurred using that item.");
        }
    }
}