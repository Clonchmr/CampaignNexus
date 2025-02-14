using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class Item 
{
    public int Id { get; set ;}
    [Required]
    public string ItemName { get; set; }
    [Required]
    public string ItemType { get; set; }
    [Required]
    public string ItemDescription { get; set; }
    public string Damage { get; set; }
    public int? ArmorClass { get; set; }
    public double Weight { get; set; }
    public string Notes { get; set; }
    public ICollection<CharacterItem> CharacterItems { get; set; }
}