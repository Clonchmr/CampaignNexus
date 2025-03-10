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
public class CharacterController : ControllerBase
{
    private CampaignNexusDbContext _dbContext;
    public CharacterController(CampaignNexusDbContext context)
    {
        _dbContext = context;
    }

    //Gets all characters for either a user, or a campaign
    [HttpGet]
    [Authorize]
    public IActionResult GetCharacters([FromQuery] int? userId, [FromQuery] int? campaignId, [FromQuery] int? count)
    {
        try
        {
            IQueryable<Character> query = _dbContext
            .Characters
            .Include(c => c.UserProfile)
                .ThenInclude(up => up.IdentityUser)
            .Include(c => c.CharacterCampaigns)
            .Include(c => c.Class);
            

            //Checks to see if userId has a value, and if it does makes sure the user exists, and adds to the query
            if (userId.HasValue)
            {
                UserProfile user = _dbContext
                .UserProfiles
                .SingleOrDefault(up => up.Id == userId.Value);

                if (user == null)
                {
                    return NotFound("That user does not exist");
                }
                else
                {
                    query = query.Where(c => c.UserId == userId.Value);
                }
            }

            //Checks to see if campaignId has a value, and if it does makes sure the campaign exists, and adds to the query
            if (campaignId.HasValue)
            {
                Campaign campaign = _dbContext
                .Campaigns
                .SingleOrDefault(c => c.Id == campaignId.Value);

                if (campaign == null)
                {
                    return NotFound("That campaign does not exist");
                }
                else
                {
                    query = query.Where(character => character.CharacterCampaigns.Any(cc => cc.CampaignId == campaignId.Value));
                }
            }

            //Checks to see if count has a value, and if so adds to the query
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return Ok(query
            .Select(c => new CharacterDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                UserProfile = campaignId.HasValue ? new UserProfileDTO
                {
                    Id = c.UserProfile.Id,
                    FirstName = c.UserProfile.FirstName,
                    LastName = c.UserProfile.LastName,
                    UserName = c.UserProfile.IdentityUser.UserName
                } : null,
                Name = c.Name,
                SpeciesId = c.SpeciesId,
                Species = new SpeciesDTO 
                {
                    Id = c.SpeciesId,
                    SpeciesName = c.Species.SpeciesName   
                }, 
                ClassId = c.ClassId,
                Class = new ClassDTO
                {
                    Id = c.Class.Id,
                    ClassName = c.Class.ClassName
                },
                Level = c.Level,
                CharacterPicUrl = c.CharacterPicUrl
            }));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetCharacters {ex}");
            return StatusCode(500, "An error occurred while retrieving characters");
        }
    }

    //Gets a character by its id
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        try
        {
            Character character = _dbContext
                .Characters
                .Include(c => c.Species)
                .Include(c => c.Class)
                    .ThenInclude(cl => cl.SubClasses)
                .Include(c => c.Alignment)
                .Include(c => c.CharacterItems)
                    .ThenInclude(ci => ci.Item)
                .Include(c => c.CharacterAbilities)
                    .ThenInclude(ca => ca.Ability)
                .Include(c => c.CharacterCampaigns)
                    .ThenInclude(cc => cc.Campaign)
                .Include(c => c.UserProfile)
                    .ThenInclude(up => up.IdentityUser)
                .SingleOrDefault(c => c.Id == id);

            //ensure the character exists
            if (character == null)
            {
                return NotFound(new {message = "That character does not exist"});
            }

            return Ok(new CharacterDTO
            {
                Id = character.Id,
                UserId = character.UserId,
                UserProfile = new UserProfileDTO
                {
                    Id = character.UserProfile.Id,
                    FirstName = character.UserProfile.FirstName,
                    LastName = character.UserProfile.LastName,
                    Email = character.UserProfile.IdentityUser.Email,
                    UserName = character.UserProfile.IdentityUser.UserName
                },
                Name = character.Name,
                Height = character.Height,
                Weight = character.Weight,
                Gender = character.Gender,
                Age = character.Age,
                Faith = character.Faith,
                SpeciesId = character.SpeciesId,
                Species = new SpeciesDTO 
                {
                    Id = character.SpeciesId,
                    SpeciesName = character.Species.SpeciesName,
                    Speed = character.Species.Speed
                },
                ClassId = character.ClassId,
                Class = new ClassDTO
                {
                    Id = character.ClassId,
                    ClassName = character.Class.ClassName,
                    HitDie = character.Class.HitDie,
                    SubClasses = character.Class.SubClasses.Select(sc => new SubClassDTO
                    {
                        Id = sc.Id,
                        Name = sc.Name,
                        Description = sc.Description
                    }).ToList()
                },
                SubClassId = character.SubClassId,
                SubClass = character.SubClassId != null ? new SubClassDTO
                {
                    Id = character.SubClass.Id,
                    Name = character.SubClass.Name
                }: null,
                Level = character.Level,
                HitPoints = character.HitPoints,
                Strength = character.Strength,
                StrengthModifier = character.StrengthModifier,
                Dexterity = character.Dexterity,
                DexterityModifier = character.DexterityModifier,
                Constitution = character.Constitution,
                ConstitutionModifier = character.ConstitutionModifier,
                Wisdom = character.Wisdom,
                WisdomModifier = character.WisdomModifier,
                Intelligence = character.Intelligence,
                IntelligenceModifier = character.IntelligenceModifier,
                Charisma = character.Charisma,
                CharismaModifier = character.CharismaModifier,
                AlignmentId = character.AlignmentId,
                Alignment = new AlignmentDTO
                {
                    Id = character.AlignmentId,
                    Name = character.Alignment.Name
                },
                Backstory = character.Backstory,
                CharacterPicUrl = character.CharacterPicUrl,
                RollForHp = character.RollForHp,
                CharacterItems = character.CharacterItems != null ? character.CharacterItems.Select(ci => new CharacterItemDTO
                {
                    Id = ci.Id,
                    ItemId = ci.ItemId,
                    Item = new ItemDTO
                    {
                        Id = ci.ItemId,
                        ItemName = ci.Item.ItemName,
                        ItemDescription = ci.Item.ItemDescription,
                        Damage = ci.Item.Damage,
                        ItemType = ci.Item.ItemType,
                        ArmorClass = ci.Item.ArmorClass,
                        Weight = ci.Item.Weight,
                        Notes = ci.Item.Notes
                    },
                    Quantity = ci.Quantity,
                    IsEquipped = ci.IsEquipped
                }).ToList() : null,
                CharacterAbilities = character.CharacterAbilities.Select(ca => new CharacterAbilityDTO
                {
                    AbilityId = ca.AbilityId,
                    Ability = new AbilityDTO
                    {
                        Id = ca.AbilityId,
                        AbilityName = ca.Ability.AbilityName,
                        AbilityDescription = ca.Ability.AbilityDescription,
                        AbilityType = ca.Ability.AbilityType,
                        DiceNumber = ca.Ability.DiceNumber,
                        NumberOfDice = ca.Ability.NumberOfDice,
                        CastingTime = ca.Ability.CastingTime,
                        Range = ca.Ability.Range,
                        SavingThrow = ca.Ability.SavingThrow,
                        Notes = ca.Ability.Notes
                    }
                }).ToList()
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in GetById {ex}");
            return StatusCode(500, "An error occurred fetching this character");
        }
    }

     [HttpPost]
     [Authorize]
     public IActionResult NewCharacter([FromBody] CharacterDTO character)
     {
         try
         {
            //Find the user and ensure they exist
             UserProfile user = _dbContext
            .UserProfiles
            .SingleOrDefault(up => up.Id == character.UserId);

            if (user == null)
            {
                return BadRequest("That user does not exist");
            }

            //Load class so HitPoints can be initialized on creation
            Class characterClass = _dbContext
            .Classes
            .SingleOrDefault(c => c.Id == character.ClassId);

            if (characterClass == null)
            {
                return BadRequest("Class does not exist");
            }

            Character characterToAdd = new Character
            {
                UserId = character.UserId,
                Name = character.Name,
                Height = character.Height,
                Weight = character.Weight,
                Gender = character.Gender,
                Age = character.Age,
                Faith = character.Faith,
                SpeciesId = character.SpeciesId,
                ClassId = character.ClassId,
                Class = characterClass,
                RollForHp = character.RollForHp,
                Strength = character.Strength,
                Dexterity = character.Dexterity,
                Constitution = character.Constitution,
                Wisdom = character.Wisdom,
                Intelligence = character.Intelligence,
                Charisma = character.Charisma,
                AlignmentId = character.AlignmentId,
                Backstory = character.Backstory,
                CharacterPicUrl = character.CharacterPicUrl
            };

            _dbContext.Characters.Add(characterToAdd);

            _dbContext.SaveChanges();

            var characterAbilities = character.CharacterAbilities.ToList().Select(ability => new CharacterAbility
            {
                CharacterId = characterToAdd.Id,
                AbilityId = ability.AbilityId
            }).ToList();

            _dbContext.CharacterAbilities.AddRange(characterAbilities);
            _dbContext.SaveChanges();

            return Created($"/api/character/{characterToAdd.Id}", characterToAdd);

         }
         catch (Exception ex)
         {
            Console.Error.WriteLine($"Error in NewCharacter {ex}");
            return StatusCode(500, "An error occurred creating that character");
         }
     }

     [HttpPut("update/{id}")]
     [Authorize]
     public IActionResult UpdateCharacter(int id, UpdateCharacterDTO character)
     {
        try
        {
            Character foundCharacter = _dbContext
            .Characters
            .SingleOrDefault(c => c.Id == id);

            if (foundCharacter == null)
            {
                return NotFound("That character does not exist");
            }

            foundCharacter.Name = character.Name;
            foundCharacter.Weight = character.Weight;
            foundCharacter.Gender = character.Gender;
            foundCharacter.AlignmentId = character.AlignmentId;
            foundCharacter.Faith = character.Faith;
            foundCharacter.Backstory = character.Backstory;
            foundCharacter.CharacterPicUrl = character.CharacterPicUrl;

            _dbContext.SaveChanges();

            return NoContent();
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error in UpdateCharacter {ex}");
            return StatusCode(500, "There was an error updating that character");
        }
     }

     [HttpPut("level/{id}")]
     [Authorize]
     public IActionResult LevelUp(int id, LevelUpDTO character)
     {
        try
        {
            Character foundCharacter = _dbContext
            .Characters
            .SingleOrDefault(c => c.Id == id);

            if (foundCharacter == null)
            {
                return NotFound("That character does not exist");
            }

            foundCharacter.Strength = character.Strength;
            foundCharacter.Dexterity = character.Dexterity;
            foundCharacter.Constitution = character.Constitution;
            foundCharacter.Wisdom = character.Wisdom;
            foundCharacter.Intelligence = character.Intelligence;
            foundCharacter.Charisma = character.Charisma;
            foundCharacter.SubClassId = character.SubClassId;
            foundCharacter.Level = character.Level;
            foundCharacter.HitPoints = character.HitPoints;

            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in LevelUp {ex}");
            return StatusCode(500, "There was an error leveling up this character");
        }
     }

     [HttpDelete("{id}")]
     [Authorize]
     public IActionResult DeleteCharacter(int id)
     {
        try
        {
            //Ensures the character to be deleted exists
            Character character = _dbContext
            .Characters
            .Include(c => c.UserProfile)
            .SingleOrDefault(c => c.Id == id);

            if (character == null) {
                return NotFound("That character does not exist");
            }

             //Finds the logged in user and gets their UserProfile
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _dbContext
            .UserProfiles
            .SingleOrDefault(up => up.IdentityUserId == identityUserId);

            if (profile.Id != character.UserId)
            {
                return Forbid();
            }

            _dbContext.Characters.Remove(character);
            _dbContext.SaveChanges();

            return NoContent();

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in DeleteCharacter {ex}");
            return StatusCode(500, "There was an error deleting that character");
        }
     }
}