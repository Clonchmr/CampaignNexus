using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class SubClass
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
}