namespace CampaignNexus.Models.DTOs;

public class ClassDTO
{
    public int Id { get; set; }
    public string ClassName { get; set; }
    public int HitDie { get; set; }
    public ICollection<SubClassDTO> SubClasses { get; set; }
    public ICollection<ClassAbilityDTO> ClassAbilities { get; set; }
}