using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CampaignNexus.Models.DTOs;

public class UserProfileDTO
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string IdentityUserId { get; set; }
    public IdentityUser IdentityUser { get; set; }
}