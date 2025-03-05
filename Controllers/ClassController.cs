using CampaignNexus.Data;
using CampaignNexus.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public ClassController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    //Get all classes with their available abilities
    [HttpGet]
    [Authorize]
    public IActionResult GetAllClasses()
    {
        try
        {
            return Ok(_dbContext
            .Classes
            .Include(c => c.ClassAbilities)
                .ThenInclude(ca => ca.Ability)
            .Select(c => new ClassDTO
            {
                Id = c.Id,
                ClassName = c.ClassName,
                HitDie = c.HitDie,
                ClassAbilities = c.ClassAbilities.Select(ca => new ClassAbilityDTO
                {
                    Id = ca.Id,
                    AbilityId = ca.AbilityId,
                    Ability = new AbilityDTO
                    {
                        Id = ca.Ability.Id,
                        AbilityName = ca.Ability.AbilityName,
                        AbilityType = ca.Ability.AbilityType,
                        AbilityDescription  =ca.Ability.AbilityDescription,
                        DiceNumber = ca.Ability.DiceNumber,
                        NumberOfDice = ca.Ability.NumberOfDice,
                        CastingTime = ca.Ability.CastingTime,
                        Range = ca.Ability.Range,
                        SavingThrow = ca.Ability.SavingThrow,
                        Notes = ca.Ability.Notes
                    },
                    ClassId = c.Id
                }).ToList()
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetAllClasses {ex}");
            return StatusCode(500, "An error occurred fetching all classes");
        }
    }
}