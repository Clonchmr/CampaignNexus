using CampaignNexus.Data;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public UserProfileController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_dbContext
            .UserProfiles
            .Include(up => up.IdentityUser)
            .Select(up => new UserProfileDTO
            {
                Id = up.Id,
                FirstName = up.FirstName,
                LastName = up.LastName,
                Email = up.IdentityUser.Email,
                UserName = up.IdentityUser.UserName
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetAll {ex}");
            return StatusCode(500, "There was an error getting users");
        }
    }
}