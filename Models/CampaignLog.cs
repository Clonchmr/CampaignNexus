using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models;

public class CampaignLog
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}