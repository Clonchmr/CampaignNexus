using CampaignNexus.Data;
using CampaignNexus.Models;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public CharacterController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    //Gets all characters for either a user, or a campaign
    [HttpGet]
    [Authorize]
    public IActionResult GetCharacters([FromQuery] int? userId, [FromQuery] int? campaignId)
    {
        try
        {
            IQueryable<Character> query = _dbContext
            .Characters
            .Include(c => c.UserProfile)
                .ThenInclude(up => up.IdentityUser)
            .Include(c => c.CharacterCampaigns)
            .Include(c => c.Class);
            

            //Checks to see if userId has a value, and if it does makes sure the user exists, and adds to the query
            if (userId.HasValue)
            {
                UserProfile user = _dbContext
                .UserProfiles
                .SingleOrDefault(up => up.Id == userId.Value);

                if (user == null)
                {
                    return NotFound("That user does not exist");
                }
                else
                {
                    query = query.Where(c => c.UserId == userId.Value);
                }
            }

            //Checks to see if campaignId has a value, and if it does makes sure the campaign exists, and adds to the query
            if (campaignId.HasValue)
            {
                Campaign campaign = _dbContext
                .Campaigns
                .SingleOrDefault(c => c.Id == campaignId.Value);

                if (campaign == null)
                {
                    return NotFound("That campaign does not exist");
                }
                else
                {
                    query = query.Where(character => character.CharacterCampaigns.Any(cc => cc.CampaignId == campaignId.Value));
                }
            }

            return Ok(query
            .Select(c => new CharacterDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                UserProfile = campaignId.HasValue ? new UserProfileDTO
                {
                    Id = c.UserProfile.Id,
                    FirstName = c.UserProfile.FirstName,
                    LastName = c.UserProfile.LastName,
                    UserName = c.UserProfile.IdentityUser.UserName
                } : null,
                Name = c.Name,
                ClassId = c.ClassId,
                Class = new ClassDTO
                {
                    Id = c.Class.Id,
                    ClassName = c.Class.ClassName
                },
                CharacterPicUrl = c.CharacterPicUrl
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetCharacters {ex}");
            return StatusCode(500, "An error occurred while retrieving characters");
        }
    }
}