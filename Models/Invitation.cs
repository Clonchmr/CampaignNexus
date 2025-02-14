using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignNexus.Models;

public class Invitation
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    [ForeignKey("SenderId")]
    public UserProfile Sender { get; set; }
    public int RecipientId { get; set; }
    [ForeignKey("RecipientId")]
    public UserProfile Recipient { get; set; }
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    public DateTime DateSent { get; set; } = DateTime.Now;
    [Required]
    public string Status { get; set; } = "Pending";
}