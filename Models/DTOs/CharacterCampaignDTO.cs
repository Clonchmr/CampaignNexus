namespace CampaignNexus.Models.DTOs;

public class CharacterCampaignDTO
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public CharacterDTO Character { get; set; }
    public int CampaignId { get; set; }
    public CampaignDTO Campaign { get; set; }
}