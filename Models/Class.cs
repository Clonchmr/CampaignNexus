using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class Class 
{
    public int Id { get; set; }
    [Required]
    public string ClassName { get; set; }
    public int HitDie { get; set; }
    public ICollection<SubClass> SubClasses { get; set; }
    public ICollection<ClassAbility> ClassAbilities { get; set; }
}