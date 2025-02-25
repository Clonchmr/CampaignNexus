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

    //Gets invitations to a campaign that are pending
    [HttpGet("campaign/{id}")]
    [Authorize]
    public IActionResult GetPending(int id) 
    {
        try
        {
            Campaign campaign = _dbContext
            .Campaigns
            .SingleOrDefault(c => c.Id == id);

            if (campaign == null)
            {
                return NotFound(new{message = "That campaign does not exist"});
            }

            return Ok(_dbContext
            .Invitations
            .Include(i => i.Recipient)
                .ThenInclude(r => r.IdentityUser)
            .Where(i => i.CampaignId == id)
            .Where(i => i.Status == "Pending")
            .OrderByDescending(i => i.DateSent)
            .Select(i => new InvitationDTO
            {
                Id = i.Id,
                SenderId = i.SenderId,
                RecipientId = i.RecipientId,
                Recipient = new UserProfileDTO
                {
                    Id = i.RecipientId,
                    FirstName = i.Recipient.FirstName,
                    LastName = i.Recipient.LastName,
                    Email = i.Recipient.IdentityUser.Email,
                    UserName = i.Recipient.IdentityUser.UserName       
                },
                CampaignId = i.CampaignId,
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
}