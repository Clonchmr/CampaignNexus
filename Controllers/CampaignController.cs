using CampaignNexus.Data;
using CampaignNexus.Models;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CampaignController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public CampaignController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    //Gets all Campaigns by a User
    //Takes an optional Query String Parameter for how many you want back
    //Can also take an optional boolean value for if a campaign has an EndDate or not
    [HttpGet("user/{userId}")]
    [Authorize]
    public IActionResult GetByUser(int userId, [FromQuery] int? count, [FromQuery] bool? showActive)
    {
        try
        {
            //Ensure the User exists
            bool userExists = _dbContext.UserProfiles.Any(up => up.Id == userId);
            if (!userExists)
            {
                return NotFound("That user does not exist");
            }

            if (count.HasValue && count <= 0)
            {
                return BadRequest("Please ensure count is a positive integer");
            }
            

            IEnumerable<Campaign> campaigns = _dbContext
            .Campaigns
            .Include(c => c.Characters)
            .Where(c => c.OwnerId == userId);
            
            if (showActive.HasValue)
            {
                campaigns = campaigns
                .Where(c => showActive.Value ? c.EndDate == null : c.EndDate != null);
            }

            campaigns = campaigns.OrderByDescending(c => c.StartDate);
            
            if (count.HasValue)
            {
                campaigns = campaigns.Take(count.Value);
            }  

            return Ok(campaigns
            .Select(c => new CampaignDTO
            {
                Id = c.Id,
                OwnerId = c.OwnerId,
                Owner = null,
                CampaignName = c.CampaignName,
                CampaignDescription = c.CampaignDescription,
                LevelRange = c.LevelRange,
                StartDate = c.StartDate,
                EndDate = c.EndDate != null ? c.EndDate : null,
                CampaignPicUrl = c.CampaignPicUrl,
                Characters = c.Characters.Select(character => new CharacterDTO
                {
                    Id = character.Id
                }).ToList()
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetByUser: {ex}");

            return StatusCode(500, "An error occurred processing your request.");
        }
    }
}