using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class Species {
    public int Id { get; set; }
    [Required]
    public string SpeciesName { get; set; }
    public int Speed { get; set; }
    [Required]
    public string Description { get; set; }
}