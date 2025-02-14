using System.ComponentModel.DataAnnotations;

namespace CampaignNexus.Models.DTOs;

public class CampaignLogDTO
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public CampaignDTO Campaign { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
    public DateTime Date { get; set; }

}