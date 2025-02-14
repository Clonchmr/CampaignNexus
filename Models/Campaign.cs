using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignNexus.Models;

public class Campaign
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public UserProfile Owner { get; set; }
    [Required]
    public string CampaignName { get; set; }
    [Required]
    public string CampaignDescription { get; set; } 
    [Required]
    public string LevelRange { get; set; }
    public DateTime StartDate { get; set; } =  DateTime.Now;
    public DateTime? EndDate { get; set; }
    public string CampaignPicUrl { get; set; }
    public ICollection<Character> Characters { get; set; }
    public ICollection<CampaignLog> CampaignLogs { get; set; }
}