using CampaignNexus.Data;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlignmentController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public AlignmentController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllAlignments()
    {
        try
        {
            return Ok(_dbContext
            .Alignments
            .Select(a => new AlignmentDTO
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetAllAlignments {ex}");
            return StatusCode(500, "An error occurred fetching alignments");
        }
    }
}