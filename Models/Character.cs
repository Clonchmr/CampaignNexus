using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignNexus.Models;

public class Character 
{
    private static int AbilityScoreModifier(int abilityScore) //calculate ability score modifiers
    {
        return (abilityScore - 10) /2;
    }
    public int Id { get; set; }
    [ForeignKey("UserProfile")]
    public int UserId { get; set; }
    public UserProfile UserProfile { get; set ;}
    [Required]
    public string Name { get; set; }
    [Required]
    public string Height { get; set; }
    [Required]
    public string Weight { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string Faith { get; set; }
    public int SpeciesId { get; set; }
    public Species Species { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
    public int? SubClassId { get; set ;} = null;
    public SubClass SubClass { get; set; }
    public int Level { get; set; } = 1;
    private int _hitPoints;
    public int HitPoints 
    {
        get => _hitPoints;
        set => _hitPoints = value;
    }
    
    public int Strength { get; set; } = 10;
    public int StrengthModifier => AbilityScoreModifier(Strength);
    public int Dexterity { get; set; } = 10;
    public int DexterityModifier => AbilityScoreModifier(Dexterity);
    public int Constitution { get; set; } = 10;
    public int ConstitutionModifier => AbilityScoreModifier(Constitution);
    public void InitializeHitPoints() //ensures class is initialized before attempting to access HitDie
    {
        if (Class == null) throw new InvalidOperationException("Class must be assigned before rolling hit-points");

         Random random = new Random();
        _hitPoints = random.Next(1, Class.HitDie + 1) + ConstitutionModifier;
    }
    public int Wisdom { get; set; } = 10;
    public int WisdomModifier => AbilityScoreModifier(Wisdom);
    public int Intelligence { get; set; } = 10;
    public int IntelligenceModifier => AbilityScoreModifier(Intelligence); 
    public int Charisma { get; set; } = 10;
    public int CharismaModifier => AbilityScoreModifier(Charisma);
    private static int ValidateAbilityScore(int score) //ensures entered ability scores are between 3 and 18
    {
        if (score < 3 || score > 18)
        {
            throw new ArgumentException("Ability scores must be between 3 and 18.");
        }
        return score;
    }
    public Character(int strength = 10, int dexterity = 10, int constitution = 10, 
                    int wisdom = 10, int intelligence = 10, int charisma = 10)
    {
        Strength = ValidateAbilityScore(strength);
        Dexterity = ValidateAbilityScore(dexterity);
        Constitution = ValidateAbilityScore(constitution);
        Wisdom = ValidateAbilityScore(wisdom);
        Intelligence = ValidateAbilityScore(intelligence);
        Charisma = ValidateAbilityScore(charisma);
    }
    public int AlignmentId { get; set; }
    public Alignment Alignment { get; set; }
    public string Backstory { get; set; }
    public string CharacterPicUrl { get; set; }
    public ICollection<CharacterItem> CharacterItems { get; set; }
    public ICollection<Item> Items { get; set; }
    public ICollection<CharacterAbility> CharacterAbilities { get; set; }
    public ICollection<Ability> Abilities { get; set; }
    public ICollection<CharacterCampaign> CharacterCampaigns { get; set; }
    public ICollection<Campaign> Campaigns { get; set; }
}