namespace CampaignNexus.Models.DTOs;

public class ClassAbilityDTO
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public ClassDTO Class { get; set; }
    public int AbilityId { get; set; }
    public AbilityDTO Ability { get; set; }
}