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
    //Gets a single CampaignLog
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        try
        {
            CampaignLog foundLog = _dbContext
            .CampaignLogs
            .SingleOrDefault(l => l.Id == id);

            if (foundLog == null)
            {
                return NotFound("That Log does not exist");
            }

            return Ok(new CampaignLogDTO
            {
                Id = foundLog.Id,
                CampaignId = foundLog.CampaignId,
                Title = foundLog.Title,
                Body = foundLog.Body,
                Date = foundLog.Date
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetById {ex}");
            return StatusCode(500, "There was an error retrieving that log");
        }
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

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateLog(int id, [FromBody] CampaignLogDTO log)
    {
        try
       { 
            CampaignLog logToUpdate = _dbContext
            .CampaignLogs.
            SingleOrDefault(l => l.Id == id);

            if (logToUpdate == null)
            {
                return NotFound("That Log does not exist");
            }

            if (string.IsNullOrEmpty(log.Title) || string.IsNullOrEmpty(log.Body))
            {
                return BadRequest("Required fields are missing");
            }

            logToUpdate.Title = log.Title;
            logToUpdate.Body = log.Body;

            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in UpdateLog: {ex}");
            return StatusCode(500, "There was an error updating that log");
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteLog(int id)
    {
        try
        {
            CampaignLog logToDelete = _dbContext
            .CampaignLogs
            .SingleOrDefault(l => l.Id == id);

            if (logToDelete == null)
            {
                return NotFound("That Log does not exist");
            }

            _dbContext.CampaignLogs.Remove(logToDelete);
            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in DeleteLog: {ex}");
            return StatusCode(500, "There was an error deleting that Log");
        }
    }
}