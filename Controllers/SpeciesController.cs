using CampaignNexus.Data;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeciesController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public SpeciesController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllSpecies()
    {
        try
        {
            return Ok(_dbContext
            .Species
            .Select(s => new SpeciesDTO
            {
                Id = s.Id,
                SpeciesName = s.SpeciesName,
                Speed = s.Speed,
                Description = s.Description
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetAllSpecies {ex}");
            return StatusCode(500, "An error occurred fetching species");
        }
    }
}