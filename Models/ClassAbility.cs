namespace CampaignNexus.Models;

public class ClassAbility
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
    public int AbilityId { get; set; }
    public Ability Ability { get; set; }
}