namespace CampaignNexus.Models.DTOs;

public class CharacterAbilityDTO
{
    public int AbilityId { get; set; }
    public AbilityDTO Ability { get; set; }
    public int CharacterId { get; set; }
    public CharacterDTO Character { get; set; }
}