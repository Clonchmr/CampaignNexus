namespace CampaignNexus.Models;

public class CharacterCampaign
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
    
}