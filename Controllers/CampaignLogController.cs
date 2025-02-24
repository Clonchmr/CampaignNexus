using CampaignNexus.Data;
using CampaignNexus.Models;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignLogController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;

    public CampaignLogController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    //Creates a new CampaignLog
    [HttpPost]
    [Authorize]
    public IActionResult NewLog([FromBody] CampaignLogDTO log)
    {
        try
        {
             //Make sure that campaign exists
           if (!_dbContext.Campaigns.Any(c => c.Id == log.CampaignId))
           {
                return BadRequest("That campaign does not exist");
           }

            //Then make sure the required fields are filled
           if (string.IsNullOrEmpty(log.Title) || string.IsNullOrEmpty(log.Body))
           {
                return BadRequest("Missing required field");
           }

            CampaignLog newLog = new CampaignLog
            {
                CampaignId = log.CampaignId,
                Title = log.Title,
                Body = log.Body
            };

           _dbContext.CampaignLogs.Add(newLog);
           _dbContext.SaveChanges();

           return Created($"api/campaignlog/{newLog.Id}", newLog);

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in NewLog {ex}");
            return StatusCode(500, "There was an error creating this log");
        }
    }
}