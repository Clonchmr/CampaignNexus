using CampaignNexus.Data;
using CampaignNexus.Models;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}