namespace CampaignNexus.Models;

public class CharacterAbility 
{
    public int AbilityId { get; set; }
    public Ability Ability { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; }
}