namespace CampaignNexus.Models.DTOs;

public class AbilityDTO
{
    public int Id { get; set; }
    public string AbilityName { get; set; }
    public string AbilityType { get; set; }
    public string AbilityDescription { get; set; }
    public int DiceNumber { get; set; }
    public int NumberOfDice { get; set; }
    public string CastingTime { get; set; }
    public string Range { get; set; }
    public string SavingThrow { get; set; }
    public string Notes { get; set; }
    public ICollection<ClassDTO> Classes { get; set; }
}