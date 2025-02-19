namespace CampaignNexus.Models;

public class CharacterItem
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; } = false;
}