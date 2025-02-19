using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class Alignment 
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}