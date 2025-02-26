using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models.DTOs;

public class CharacterDTO 
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserProfileDTO UserProfile { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Height { get; set; }
    [Required]
    public string Weight { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string Faith { get; set; }
    public string SpeciesId { get; set; }
    public SpeciesDTO Species { get; set; }
    public int ClassId { get; set; }
    public ClassDTO Class { get; set; }
    public int? SubClassId { get; set; }
    public SubClassDTO SubClass { get; set; }
    public int Level { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int StrengthModifier { get; set; }
    public int Dexterity { get; set; }
    public int DexterityModifier { get; set; }
    public int Constitution { get; set; }
    public int ConstitutionModifier { get; set; }
    public int Wisdom { get; set; }
    public int WisdomModifier { get; set; }
    public int Intelligence { get; set; }
    public int IntelligenceModifier { get; set; }
    public int Charisma { get; set; }
    public int CharismaModifier { get; set; }
    public int AlignmentId { get; set; }
    public AlignmentDTO Alignment { get; set; }
    public string Backstory { get; set; }
    public string CharacterPicUrl { get; set; }
    public ICollection<CharacterItemDTO> CharacterItems { get; set; }
    public ICollection<ItemDTO> Items { get; set; }
    public ICollection<CharacterAbilityDTO> CharacterAbilities { get; set; }
    public ICollection<AbilityDTO> Abilities { get; set; }
    public ICollection<CampaignDTO> Campaigns { get; set; }

    public double TotalWeight {
        get 
        {
            if (CharacterItems != null)
            {
            return CharacterItems.Sum(i => i.Item.Weight * i.Quantity);
            } 
            else
            {
                return 0;
            }
        }
    }
}