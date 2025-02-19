using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class Ability
{
    public int Id { get; set; }
    [Required]
    public string AbilityName { get; set; }
    [Required]
    public string AbilityType { get; set; }
    [Required]
    public string AbilityDescription { get; set; }
    public int? DiceNumber { get; set ;}
    public int? NumberOfDice { get; set; }
    public string CastingTime { get; set; }
    [Required]
    public string Range { get; set; }
    public string SavingThrow { get; set; }
    public string Notes { get; set; }
    public ICollection<ClassAbility> ClassAbilities { get; set; }
    public ICollection<CharacterAbility> CharacterAbilities { get; set; }
}