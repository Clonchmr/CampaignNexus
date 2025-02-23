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

    //Gets a campaign by its Id
    //Includes Characters, and Logs
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id) 
    {
        try
        {
            Campaign campaign = _dbContext
            .Campaigns
            .Include(c => c.CampaignLogs)
            .Include(c => c.Owner)
                .ThenInclude(o => o.IdentityUser)
            .Include(c => c.CharacterCampaigns)
                .ThenInclude(cc => cc.Character)
                    .ThenInclude(character => character.UserProfile)
                        .ThenInclude(up => up.IdentityUser)
            .SingleOrDefault(c => c.Id == id);

            if (campaign == null)
            {
                return NotFound("That campaign does not exist");
            }

            return Ok(new CampaignDTO
            {
                Id = id,
                OwnerId = campaign.OwnerId,
                Owner = new UserProfileDTO
                {
                    Id = campaign.OwnerId,
                    FirstName = campaign.Owner.FirstName,
                    LastName = campaign.Owner.LastName,
                    Email = campaign.Owner.IdentityUser.Email,
                    UserName = campaign.Owner.IdentityUser.UserName
                },
                CampaignName = campaign.CampaignName,
                CampaignDescription = campaign.CampaignDescription,
                LevelRange = campaign.LevelRange,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate != null ? campaign.EndDate : null,
                CampaignPicUrl = campaign.CampaignPicUrl,
                Characters = campaign.Characters != null ? campaign.Characters.Select(character => new CharacterDTO
                {
                    Id = character.Id,
                    UserId = character.UserId,
                    UserProfile = new UserProfileDTO
                    {
                        Id = character.UserProfile.Id,
                        FirstName = character.UserProfile.FirstName,
                        LastName = character.UserProfile.LastName,
                        Email = character.UserProfile.IdentityUser.Email,
                        UserName = character.UserProfile.IdentityUser.Email
                    },
                    Name = character.Name
                }).ToList() : null,
                CampaignLogs = campaign.CampaignLogs != null ? campaign.CampaignLogs.Select(l => new CampaignLogDTO
                {
                    Id = l.Id,
                    CampaignId = id,
                    Title = l.Title,
                    Body = l.Body,
                    Date = l.Date
                }).ToList() : null
            });
            
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error in GetById {ex}");
            return StatusCode(500, "There was an error processing your request");
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult NewCampaign([FromBody] CampaignDTO campaign)
    {
        try
        {
            if (campaign == null || 
            string.IsNullOrEmpty(campaign.CampaignName) || 
            string.IsNullOrEmpty(campaign.LevelRange) || 
            string.IsNullOrEmpty(campaign.CampaignDescription))
            {
                return BadRequest("Missing required field");
            }

            Campaign newCampaign = new Campaign
            {
                OwnerId = campaign.OwnerId,
                CampaignName = campaign.CampaignName,
                CampaignDescription = campaign.CampaignDescription,
                LevelRange = campaign.LevelRange,
                CampaignPicUrl = campaign.CampaignPicUrl
            };

            _dbContext.Campaigns.Add(newCampaign);
            _dbContext.SaveChanges();

            return Created($"/api/campaign/{newCampaign.Id}", newCampaign);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in NewCampaign {ex}");
            return StatusCode(500, "There was an error processing your request");
        }
    }
}