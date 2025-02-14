namespace CampaignNexus.Models.DTOs;

public class CharacterItemDTO
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public ItemDTO Item { get; set; }
    public int CharacterId { get; set; }
    public CharacterDTO Character { get; set; }
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; }
}