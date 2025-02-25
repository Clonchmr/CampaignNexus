using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignNexus.Models.DTOs;

public class InvitationDTO
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    [ForeignKey("SenderId")]
    public UserProfileDTO Sender { get; set; }
    public int RecipientId { get; set; }
    [ForeignKey("RecipientId")]
    public UserProfileDTO Recipient { get; set; }
    public int CampaignId { get; set; }
    public CampaignDTO Campaign { get; set; }
    public DateTime DateSent { get; set; }
    public string Status { get; set; }
}