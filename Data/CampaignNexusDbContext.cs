using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CampaignNexus.Models;

namespace CampaignNexus.Data;

public class CampaignNexusDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DbSet<UserProfile> UserProfiles { get; set; }

    public CampaignNexusDbContext(DbContextOptions<CampaignNexusDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //seed Identity Roles
        modelBuilder
            .Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = "d4cef1d2-b6b5-4f0e-b252-68461fb7b4e9",
                    Name = "Admin",
                    NormalizedName = "admin"
                },
                new IdentityRole
                {
                    Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    Name = "DM",
                    NormalizedName = "dm"
                },
                new IdentityRole
                {
                    Id = "f57b92a3-d8e9-4b85-bd0f-0f4f9b1c527b",
                    Name = "Player",
                    NormalizedName = "player"
                }
            );

        //seed Identity Users
        modelBuilder
            .Entity<IdentityUser>()
            .HasData(
                new IdentityUser
                {
                    Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                    UserName = "ClonchMr",
                    Email = "Clonch@mr.com",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                        null,
                        _configuration["AdminPassword"]
                    )
                }
        );

        //Seed Identity User Roles
        modelBuilder
            .Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "d4cef1d2-b6b5-4f0e-b252-68461fb7b4e9",
                    UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "f57b92a3-d8e9-4b85-bd0f-0f4f9b1c527b",
                    UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
                }
        );

        //Seed User Profile
        modelBuilder
            .Entity<UserProfile>()
            .HasData(
                new UserProfile
                {
                    Id = 1,
                    FirstName = "Matthew",
                    LastName = "Clonch",
                    IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
                }
            );
    }
}