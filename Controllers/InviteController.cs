using System.Security.Claims;
using CampaignNexus.Data;
using CampaignNexus.Models;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InviteController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;

    public InviteController(CampaignNexusDbContext context) 
    {
        _dbContext = context;
    }

    //Gets pending invitations to a campaign, or to a user
    [HttpGet("pending")]
    [Authorize]
    public IActionResult GetPending([FromQuery] int? campaignId, [FromQuery] int? recipientId) 
    {
        try
        {
            IQueryable<Invitation> query = _dbContext
            .Invitations
            .Include(i => i.Recipient)
                .ThenInclude(r => r.IdentityUser)
            .Include(i => i.Sender)
                .ThenInclude(s => s.IdentityUser)
            .Include(i => i.Campaign)
            .Where(i => i.Status == "Pending");

            //Checks to see if campaignId has a value, and if so makes sure that campaign exists, and then adds to the query
             if (campaignId.HasValue)
            {
                Campaign campaign = _dbContext
                .Campaigns
                .SingleOrDefault(c => c.Id == campaignId);
                
                if (campaign == null)
                {
                    return NotFound(new{message = "That campaign does not exist"});
                }
                else
                {
                    query = query.Where(i => i.CampaignId == campaignId.Value);
                }
            }

            //Checks to see if recipientId has a value, and if so makes sure that recipient exists, and then adds to the query
            if (recipientId.HasValue)
            {
                UserProfile recipient = _dbContext
                .UserProfiles
                .SingleOrDefault(up => up.Id == recipientId.Value);

                if (recipient == null)
                {
                    return NotFound(new{message = "That recipient does not exist"});
                }
                else
                {
                    query = query.Where(i => i.RecipientId == recipientId.Value);
                } 
            }

            return Ok(query
            .OrderByDescending(i => i.DateSent)
            .Select(i => new InvitationDTO
            {
                Id = i.Id,
                SenderId = i.SenderId,
                Sender = recipientId.HasValue ? new UserProfileDTO
                {
                    Id = i.Sender.Id,
                    FirstName = i.Sender.FirstName,
                    LastName = i.Sender.LastName,
                    Email = i.Sender.IdentityUser.Email,
                    UserName = i.Sender.IdentityUser.UserName
                } : null,
                RecipientId = i.RecipientId,
                Recipient = campaignId.HasValue ? new UserProfileDTO
                {
                    Id = i.RecipientId,
                    FirstName = i.Recipient.FirstName,
                    LastName = i.Recipient.LastName,
                    Email = i.Recipient.IdentityUser.Email,
                    UserName = i.Recipient.IdentityUser.UserName       
                } : null,
                CampaignId = i.CampaignId,
                Campaign = recipientId.HasValue ? new CampaignDTO
                {
                    Id = i.Campaign.Id,
                    CampaignName = i.Campaign.CampaignName,
                    LevelRange = i.Campaign.LevelRange,
                    CampaignDescription = i.Campaign.CampaignDescription,
                    CampaignPicUrl = i.Campaign.CampaignPicUrl
                } : null,
                DateSent = i.DateSent,
                Status = i.Status
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetPending {ex}");
            return StatusCode(500, "There was an error retrieving pending invites");
        }
    }



    //Send a new invitation
    [HttpPost]
    [Authorize]
    public IActionResult SendInvite([FromBody] InvitationDTO invitationDto)
    {
        try
        {
            //Find the sender and check if they exist
            UserProfile sender = _dbContext
            .UserProfiles
            .SingleOrDefault(up => up.Id == invitationDto.SenderId);

            if (sender == null)
            {
                return NotFound( new {message = "The user sending this invite does not exist"});
            }

            //Find the recipient and check if they exist
            UserProfile recipient = _dbContext
            .UserProfiles
            .SingleOrDefault(up => up.Id == invitationDto.RecipientId);

            if (recipient == null)
            {
                return NotFound(new {message = "That recipient does not exist"});
            }

            //Find the campaign and check if it exists
            Campaign campaign = _dbContext
            .Campaigns
            .SingleOrDefault(c => c.Id == invitationDto.CampaignId);

            if (campaign == null)
            {
                return NotFound(new {message = "That campaign does not exist"});
            }

            Invitation newInvitation = new Invitation
            {
                SenderId = invitationDto.SenderId,
                RecipientId = invitationDto.RecipientId,
                CampaignId = invitationDto.CampaignId
            };

            _dbContext.Invitations.Add(newInvitation);
            _dbContext.SaveChanges();

            return Created($"api/invite/{newInvitation.Id}", newInvitation);

       
        }
     catch (Exception ex)
        {
            Console.Error.WriteLine($"Error occurred in SendInvite {ex}");
            return StatusCode(500, "An error occurred sending this invite");
  
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteInvite(int id)
    {
        try
        {
              //Finds the logged in user and gets their UserProfile
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _dbContext
            .UserProfiles
            .SingleOrDefault(up => up.IdentityUserId == identityUserId);

            Invitation invite = _dbContext
            .Invitations
            .SingleOrDefault(i => i.Id == id);

            if (invite == null)
            {
                return NotFound(new { message = "That invite does not exist"});
            }

            //Checks to see if the person trying to access this endpoint is the logged in user
            if (invite.SenderId != profile.Id)
            {
                return Forbid();
            }

            _dbContext.Invitations.Remove(invite);
            _dbContext.SaveChanges();

            return NoContent();

        }
        catch (Exception ex)
        {
            Console.Error.Write($"Error in delete invite {ex}");
            return StatusCode(500, "An error occurred deleting that invitation");
        }
    }

    //Creates a new CharacterCampaign entity linking a character to a campaign
    [HttpPost("accept")]
    [Authorize]
    public IActionResult AcceptInvite([FromBody] CharacterCampaignDTO characterCampaign)
    {
        try
        {
            //Checks to see if the character exists
            Character character = _dbContext
            .Characters
            .Include(c => c.UserProfile)
            .SingleOrDefault(c => c.Id == characterCampaign.CharacterId);

            if (character == null)
            {
                return NotFound("That character does not exist");
            }
            //Checks to see if that campaign exists
            Campaign campaign = _dbContext
            .Campaigns
            .Include(c => c.Owner)
            .SingleOrDefault(c => c.Id == characterCampaign.CampaignId);

            if (campaign == null)
            {
                return NotFound("That campaign does not exist");
            }

            //Creates the new CharacterCampaign entity
            CharacterCampaign newCharacterCampaign = new CharacterCampaign
            {
                CharacterId = characterCampaign.CharacterId,
                CampaignId = characterCampaign.CampaignId
            };

            _dbContext.CharacterCampaigns.Add(newCharacterCampaign);

            //Finds the invitation thats being accepted and changes its Status to Accepted
            Invitation invite = _dbContext
            .Invitations
            .SingleOrDefault(i => i.RecipientId == character.UserProfile.Id && i.CampaignId == campaign.Id && i.Status == "Pending");

            invite.Status = "Accepted";


            _dbContext.SaveChanges();
            
            return Created($"api/charactercampaign/{newCharacterCampaign.Id}", newCharacterCampaign);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error occurred in AcceptInvite {ex}");
            return StatusCode(500, "An error occurred accepting the invite");
        }
    }

    [HttpPost("decline/{id}")]
    [Authorize]
    public IActionResult DeclineInvite(int id)
    {
        try
        {
            //Find the invite to be declined
            Invitation invite = _dbContext
            .Invitations
            .SingleOrDefault(i => i.Id == id);

            if (invite == null)
            {
                return NotFound("That invitation does not exist");
            }

            invite.Status = "Declined";

            _dbContext.SaveChanges();

            return NoContent();
            
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error occurred in DeclineInvite. {ex}");
            return StatusCode(500, "An error occurred trying to decline this invitation");
        }
    }
}