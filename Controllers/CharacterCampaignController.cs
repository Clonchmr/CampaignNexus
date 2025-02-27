using System.Security.Claims;
using CampaignNexus.Data;
using CampaignNexus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CharacterCampaignController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public CharacterCampaignController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    [HttpDelete]
    [Authorize]
    public IActionResult RemoveCharacter([FromQuery] int characterId, [FromQuery] int campaignId)
    {
        try
        {
           CharacterCampaign characterToRemove =  _dbContext
           .CharacterCampaigns
           .Include(cc => cc.Campaign)
           .Include(cc => cc.Character)
           .SingleOrDefault(cc => cc.CharacterId == characterId && cc.CampaignId == campaignId);

            //Finds the logged in user and gets their UserProfile
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _dbContext
            .UserProfiles
            .SingleOrDefault(up => up.IdentityUserId == identityUserId);
            
            //Ensures the person accessing this endpoint owns the campaign or the character
            if (profile.Id != characterToRemove.Campaign.OwnerId && profile.Id != characterToRemove.Character.UserId)
            {
                return Forbid();
            }

           if (characterToRemove == null)
           {
            return NotFound("That character does not belong to that campaign");
           }

           _dbContext.CharacterCampaigns.Remove(characterToRemove);
           _dbContext.SaveChanges();

           return NoContent();

           
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error occurred in RemoveCharacter {ex}");
            return StatusCode(500, "An error occurred removing this character");
        }
    }
}