using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CampaignNexus.Models;

public class UserProfile
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string IdentityUserId { get; set; }
    public IdentityUser IdentityUser { get; set; }
}