using System.ComponentModel.DataAnnotations;
using CampaignNexus.Data;
using CampaignNexus.Models;
using CampaignNexus.Models.DTOs;
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

    [HttpGet]
    [Authorize]
    public IActionResult GetAllItems() 
    {
        try
        {
            return Ok(_dbContext
            .Items
            .Select(i => new ItemDTO
            {
                Id = i.Id,
                ItemName = i.ItemName,
                ItemType = i.ItemType,
                ItemDescription = i.ItemDescription,
                Damage = i.Damage,
                ArmorClass = i.ArmorClass != null ? i.ArmorClass : null,
                Weight = i.Weight,
                Notes = i.Notes
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetAllItems {ex}");
            return StatusCode(500, "An error occurred retrieving items");
        }
    }

    //Creates a new CharacterItem entity, adding an item to a character
    [HttpPost("characterAdd/{itemId}")]
    [Authorize]
    public IActionResult AddToCharacter(int itemId, [FromQuery, Required] int characterId, [FromQuery, Required] int quantity)
    {
        try
        {
            //Finds the item to be added, along with the character the item is being added to, and ensures they both exist
            Item item = _dbContext
            .Items
            .SingleOrDefault(i => i.Id == itemId);

            Character character = _dbContext
            .Characters
            .SingleOrDefault(c => c.Id == characterId);

            if (item == null || character == null) 
            {
                return NotFound("That item or character does not exist");
            }

            CharacterItem newCharacterItem = new CharacterItem
            {
                ItemId = itemId,
                CharacterId = characterId,
                Quantity = quantity
            };

            _dbContext.CharacterItems.Add(newCharacterItem);
            _dbContext.SaveChanges();

            return Created($"api/characterItems/{newCharacterItem.Id}", newCharacterItem);

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in AddToCharacter {ex}");
            return StatusCode(500, "There was an error adding that item to your character");
        }
    }
}