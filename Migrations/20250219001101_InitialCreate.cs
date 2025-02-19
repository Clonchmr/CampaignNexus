﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampaignNexus.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassName = table.Column<string>(type: "text", nullable: false),
                    HitDie = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpeciesName = table.Column<string>(type: "text", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    CampaignName = table.Column<string>(type: "text", nullable: false),
                    CampaignDescription = table.Column<string>(type: "text", nullable: false),
                    LevelRange = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CampaignPicUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_UserProfiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Faith = table.Column<string>(type: "text", nullable: true),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false),
                    ClassId = table.Column<int>(type: "integer", nullable: false),
                    SubClassId = table.Column<int>(type: "integer", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    HitPoints = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    Dexterity = table.Column<int>(type: "integer", nullable: false),
                    Constitution = table.Column<int>(type: "integer", nullable: false),
                    Wisdom = table.Column<int>(type: "integer", nullable: false),
                    Intelligence = table.Column<int>(type: "integer", nullable: false),
                    Charisma = table.Column<int>(type: "integer", nullable: false),
                    AlignmentId = table.Column<int>(type: "integer", nullable: false),
                    Backstory = table.Column<string>(type: "text", nullable: true),
                    CharacterPicUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Alignments_AlignmentId",
                        column: x => x.AlignmentId,
                        principalTable: "Alignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_SubClasses_SubClassId",
                        column: x => x.SubClassId,
                        principalTable: "SubClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CampaignId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignLogs_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    CampaignId = table.Column<int>(type: "integer", nullable: false),
                    DateSent = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_UserProfiles_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_UserProfiles_SenderId",
                        column: x => x.SenderId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AbilityName = table.Column<string>(type: "text", nullable: false),
                    AbilityType = table.Column<string>(type: "text", nullable: false),
                    AbilityDescription = table.Column<string>(type: "text", nullable: false),
                    DiceNumber = table.Column<int>(type: "integer", nullable: true),
                    NumberOfDice = table.Column<int>(type: "integer", nullable: true),
                    CastingTime = table.Column<string>(type: "text", nullable: true),
                    Range = table.Column<string>(type: "text", nullable: false),
                    SavingThrow = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abilities_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CampaignCharacter",
                columns: table => new
                {
                    CampaignsId = table.Column<int>(type: "integer", nullable: false),
                    CharactersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCharacter", x => new { x.CampaignsId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_CampaignCharacter_Campaigns_CampaignsId",
                        column: x => x.CampaignsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignCharacter_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterCampaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    CampaignId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterCampaigns_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterCampaigns_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    ItemDescription = table.Column<string>(type: "text", nullable: false),
                    Damage = table.Column<string>(type: "text", nullable: true),
                    ArmorClass = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    AbilityId = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAbilities", x => new { x.CharacterId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassId = table.Column<int>(type: "integer", nullable: false),
                    AbilityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassAbilities_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsEquipped = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterItems_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "AbilityDescription", "AbilityName", "AbilityType", "CastingTime", "CharacterId", "DiceNumber", "Notes", "NumberOfDice", "Range", "SavingThrow" },
                values: new object[,]
                {
                    { 1, "On a hit, an unarmed strike deals bludgeoning damage equal to 1 + your Strength modifier.  You are proficient with your unarmed strikes. ", "Unarmed Strike", "Action, Default", "1A", null, null, "Str modifier + 1", null, "5 ft.", null },
                    { 2, "When you take the Dash action, you gain extra movement for the current turn. The increase equals your speed, after applying any modifiers. With a speed of 30 feet, for example, you can move up to 60 feet on your turn if you dash.\r\nAny increase or decrease to your speed changes this additional movement by the same amount. If your speed of 30 feet is reduced to 15 feet, for instance, you can move up to 30 feet this turn if you dash.\r\n", "Dash", "Action, Default", "1A", null, null, "Doubles Movement ", null, "Self", null },
                    { 3, "@If you take the Disengage action, your movement doesn’t provoke opportunity attacks for the rest of the turn.", "Disengage", "Action, Default", "1A", null, null, "Move without O/A", null, "Self", null },
                    { 4, "When you take the Dodge action, you focus entirely on avoiding attacks. Until the start of your next turn, any attack roll made against you has disadvantage if you can see the attacker, and you make Dexterity saving throws with advantage. You lose this benefit if you are incapacitated (as explained in Conditions ) or if your speed drops to 0.\r\n", "Dodge", "Action, Default", "1A", null, null, "Attacks against have D/A", null, "Self", null },
                    { 100, "A flash of light streaks toward a creature of your choice within range. Make a ranged spell attack against the target. On a hit, the target takes 4d6 radiant damage, and the next attack roll made against this target before the end of your next turn has advantage, thanks to the mystical dim light glittering on the target until then.", "Guiding Bolt", "Spell", "1A", null, 6, "V/S", 4, "120 ft.", null },
                    { 101, "Flame-like radiance descends on a creature that you can see within range. The target must succeed on a Dexterity saving throw or take 1d8 radiant damage. The target gains no benefit from cover for this saving throw. ", "Sacred Flame", "Cantrip", "1A", null, 8, "V/S", 1, "60 ft.", "Dex" },
                    { 102, " A creature of your choice that you can see within range regains hit points equal to 1d4 + your spellcasting ability modifier. This spell has no effect on undead or constructs.", "Healing Word", "Spell", "1BA", null, 4, "V", 1, "60 ft.", null },
                    { 103, "You touch one willing creature. Once before the spell ends, the target can roll a d4 and add the number rolled to one ability check of its choice. It can roll the die before or after making the ability check. The spell then ends.", "Guidance", "Cantrip", "1A", null, null, "V/S", null, "Touch", null },
                    { 104, "You bless up to three creatures within range. Whenever a target makes an attack roll or a saving throw before the spell ends, the target adds 1d4 to the attack roll or save.", "Bless", "Spell", "1A", null, null, "V/S/M", null, "30 ft.", null },
                    { 105, "You hurl a mote of fire at a creature or an object within range. Make a ranged spell attack against the target. On a hit, the target takes 1d10 Fire damage. A flammable object hit by this spell starts burning if it isn’t being worn or carried.", "Fire Bolt", "Cantrip", "1A", null, 10, "V/S", 1, "120 ft.", null },
                    { 106, "You unleash a wave of thunderous energy. Each creature in a 15-foot cube originating from you makes a Constitution saving throw. On a failed save, a creature takes 2d8 Thunder damage and is pushed 10 feet away from you. On a successful save, a creature takes half as much damage only.", "Thunderwave", "Spell", "1A", null, 8, "V/S", 2, "Self (15 ft. Cube)", "Con" },
                    { 107, "A creature of your choice that you can see within range perceives everything as hilariously funny and falls into fits of laughter if this spell affects it. The target must succeed on a Wisdom saving throw or fall prone, becoming incapacitated and unable to stand up for the duration. A creature with an Intelligence score of 4 or less isn’t affected.\r\nAt the end of each of its turns, and each time it takes damage, the target can make another Wisdom saving throw. The target has advantage on the saving throw if it’s triggered by damage. On a success, the spell ends.\r\n", "Tasha's Hideous Laughter", "Spell", "1A", null, null, "V/S/M", null, "30 ft.", "Wis" },
                    { 108, "You unleash a string of insults laced with subtle enchantments at a creature you can see within range. If the target can hear you (though it need not understand you), it must succeed on a Wisdom saving throw or take 1d4 psychic damage and have disadvantage on the next attack roll it makes before the end of its next turn.", "Vicious Mockery", "Cantrip", "1A", null, 4, "V", 1, "60 ft.", "Wis" },
                    { 109, "You whisper a discordant melody that only one creature of your choice within range can hear, wracking it with terrible pain. The target must make a Wisdom saving throw. On a failed save, it takes 3d6 psychic damage and must immediately use its reaction, if available, to move as far as its speed allows away from you. The creature doesn’t move into obviously dangerous ground, such as a fire or a pit. On a successful save, the target takes half as much damage and doesn’t have to move away. A deafened creature automatically succeeds on the save.", "Dissonant Whispers", "Spell", "1A", null, 6, "V", 3, "60 ft.", "Wis" },
                    { 110, "Grasping weeds and vines sprout from the ground in a 20-foot square starting from a point within range. For the duration, these plants turn the ground in the area into difficult terrain.\r\nA creature in the area when you cast the spell must succeed on a Strength saving throw or be restrained by the entangling plants until the spell ends. A creature restrained by the plants can use its action to make a Strength check against your spell save DC. On a success, it frees itself.\r\n", "Entangle", "Spell", "1A", null, null, "V/S", null, "90 ft.", "Str" },
                    { 111, "Up to ten berries appear in your hand and are infused with magic for the duration. A creature can use its action to eat one berry. Eating a berry restores 1 hit point, and the berry provides enough nourishment to sustain a creature for one day.\r\nThe berries lose their potency if they have not been consumed within 24 hours of the casting of this spell.", "GoodBerry", "Spell", "1A", null, null, "V/S/M", null, "Touch", null },
                    { 112, "Beginning at 1st level, you know how to strike subtly and exploit a foe's distraction. Once per turn, you can deal an extra 1d6 damage to one creature you hit with an attack if you have advantage on the attack roll. The attack must use a finesse or a ranged weapon.\r\n\r\nYou don't need advantage on the attack roll if another enemy of the target is within 5 feet of it, that enemy isn't incapacitated, and you don't have disadvantage on the attack roll.\r\n\r\nThe amount of the extra damage increases as you gain levels in this class.", "Sneak Attack", "Action", "1A", null, 6, null, 1, "5 ft.", null },
                    { 113, "Starting at 2nd level, your quick thinking and agility allow you to move and act quickly. You can take a bonus action on each of your turns in combat. This action can be used only to take the Dash, Disengage, or Hide action.", "Cunning Action", "Action", "1BA", null, null, "Hide, Dash, Disengage", null, "Self", null },
                    { 114, "A protective magical force surrounds you, manifesting as a spectral frost that covers you and your gear. You gain 5 temporary hit points for the duration. If a creature hits you with a melee attack while you have these hit points, the creature takes 5 cold damage.", "Armor of Agathys", "Spell", "1A", null, null, "V/S/M", null, "Self", null },
                    { 115, "You invoke the power of Hadar, the Dark Hunger. Tendrils of dark energy erupt from you and batter all creatures within 10 feet of you. Each creature in that area must make a Strength saving throw. On a failed save, a target takes 2d6 necrotic damage and can’t take reactions until its next turn. On a successful save, the creature takes half damage, but suffers no other effect.", "Arms of Hadar", "Spell", "1A", null, 6, "V/S", 2, "Self (10 ft. Radius)", "Str" },
                    { 116, "You point your finger, and the creature that damaged you is momentarily surrounded by hellish flames. The creature must make a Dexterity saving throw. It takes 2d10 fire damage on a failed save, or half as much damage on a successful one.", "Hellish Rebuke", "Spell", "Reaction", null, 10, "V/S", 2, "60 ft.", "Dex" },
                    { 117, " Starting at 2nd level, you can push yourself beyond your normal limits for a moment. On your turn, you can take one additional action.\r\n\r\nOnce you use this feature, you must finish a short or long rest before you can use it again. Starting at 17th level, you can use it twice before a rest, but only once on the same turn.", "Action Surge", "Action", "Once per Long Rest", null, null, null, null, "Self", null },
                    { 118, " When you roll a 1 or 2 on a damage die for an attack you make with a melee weapon that you are wielding with two hands, you can reroll the die and must use the new roll, even if the new roll is a 1 or a 2. The weapon must have the two-handed or versatile property for you to gain this benefit.", "Great Weapon Fighting", "Style", null, null, null, "Reroll 1’s and 2’s for damage", null, "Self", null },
                    { 119, "When you are wielding a melee weapon in one hand and no other weapons, you gain a +2 bonus to damage rolls with that weapon.", "Dueling", "Style", null, null, null, "+2 bonus damage with 1/H", null, "Self", null },
                    { 120, "The next time you hit a creature with a weapon attack before this spell ends, a writhing mass of thorny vines appears at the point of impact, and the target must succeed on a Strength saving throw or be restrained by the magical vines until the spell ends. A Large or larger creature has advantage on this saving throw. If the target succeeds on the save, the vines shrivel away.", "Ensnaring Strike", "Spell", "1BA", null, 6, "V", 1, "Self", "Str" },
                    { 121, "You touch a creature. The target’s speed increases by 10 feet until the spell ends.", "Longstrider", "Spell", "1A", null, null, "V/S/M", null, "Touch", null },
                    { 122, "You gain the ability to comprehend and verbally communicate with beasts for the duration. The knowledge and awareness of many beasts is limited by their intelligence, but at minimum, beasts can give you information about nearby locations and monsters, including whatever they can perceive or have perceived within the past day. You might be able to persuade a beast to perform a small favor for you, at the DM’s discretion.", "Speak With Animals", "Spell", "1A", null, null, "V/S 10 minutes", null, "Self", null },
                    { 123, " Starting at 2nd level, when you hit a creature with a melee weapon attack, you can expend one spell slot to deal radiant damage to the target, in addition to the weapon’s damage. The extra damage is 2d8 for a 1st-level spell slot, plus 1d8 for each spell level higher than 1st, to a maximum of 5d8. The damage increases by 1d8 if the target is an undead or a fiend, to a maximum of 6d8.", "Divine Smite", "Spell", "1A", null, 8, "2 D8 plus Weapon Damage", 2, "5 ft.", null },
                    { 124, "The first time you hit with a melee weapon attack during this spell’s duration, your weapon rings with thunder that is audible within 300 feet of you, and the attack deals an extra 2d6 thunder damage to the target. Additionally, if the target is a creature, it must succeed on a Strength saving throw or be pushed 10 feet away from you and knocked prone.", "Thunderous Smite", "Spell", "1A", null, 6, "V", 2, "Self", "Str" },
                    { 125, "The next time you hit with a melee weapon attack during this spell’s duration, your attack deals an extra 1d6 psychic damage. Additionally, if the target is a creature, it must make a Wisdom saving throw or be frightened of you until the spell ends. As an action, the creature can make a Wisdom check against your spell save DC to steel its resolve and end this spell.", "Wrathful Smite", "Spell", "1BA", null, 6, "V", 1, "Self", "Wis" },
                    { 126, "In battle, you fight with primal ferocity. On your turn, you can enter a rage as a bonus action.\r\n\r\nWhile raging, you gain the following benefits if you aren't wearing heavy armor:\r\nYou have advantage on Strength checks and Strength saving throws.\r\n\r\nWhen you make a melee weapon attack using Strength, you gain a bonus to the damage roll that increases as you gain levels as a barbarian, as shown in the Rage Damage column of the Barbarian table.\r\nYou have resistance to bludgeoning, piercing, and slashing damage.\r\nIf you are able to cast spells, you can't cast them or concentrate on them while raging.\r\nYour rage lasts for 1 minute. It ends early if you are knocked unconscious or if your turn ends and you haven't attacked a hostile creature since your last turn or taken damage since then. You can also end your rage on your turn as a bonus action.\r\n", "Rage", "Action", "1BA", null, null, null, null, "Self", null },
                    { 127, "Starting at 2nd level, you can throw aside all concern for defense to attack with fierce desperation. When you make your first attack on your turn, you can decide to attack recklessly. Doing so gives you advantage on melee weapon attack rolls using Strength during this turn, but attack rolls against you have advantage until your next turn.", "Reckless Attack", "Action", null, null, null, null, null, "Self", null },
                    { 128, "While you are not wearing any armor, your armor class equals 10 + your Dexterity modifier + your Constitution modifier. You can use a shield and still gain this benefit.", "Unarmored Defense", "Buff", null, null, null, null, null, "Self", null }
                });

            migrationBuilder.InsertData(
                table: "Alignments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, " A lawful good character is a protector. The iconic example of lawful good is a paladin, a holy knight who protects the weak and destroys evil.", "Lawful Good" },
                    { 2, "A neutral good character believes in altruism over all else.", "Neutral Good" },
                    { 3, "A Chaotic Good character believes in freedom as the highest virtue. The iconic example of chaotic good is Robin Hood, who rebels against authority as a way to protect the poor from poverty and suffering.", "Chaotic Good" },
                    { 4, "A lawful neutral character obeys principle as the highest virtue. For example, a judge who treats all fairly and equally would be considered lawful neutral.", "Lawful Neutral" },
                    { 5, "A true neutral character is neutral on both axes, and cares not for any stance of alignment. This often describes someone who cares only for their own personal needs, neither inclined to hurt or harm others, nor to obey or rebel.", "True Neutral" },
                    { 6, "A chaotic neutral character follows their heart, but without the willingness to self-sacrifice as a chaotic good character might. A great many adventurers are chaotic neutral, doing what they wish and rejecting all forms of authority. Some chaotic neutral characters follow a deliberate philosophy of destroying the old to make way for the new.", "Chaotic Neutral" },
                    { 7, " A lawful evil character is a tyrant. They have no moral qualms about punishing individuals for the greater goal of furthering society. A lawful evil villain is often easy to deal with, as they can often be trusted to keep their word.", "Lawful Evil" },
                    { 8, "A  neutral evil character is selfish, and has no problem harming others to get what they want - if they can get away with it.", "Neutral Evil" },
                    { 9, "A chaotic evil character is malevolent. A villain bent on revenge might be of this alignment. Where the most powerful lawful evil villains might aim to conquer the world, this might be preferable to their chaotic evil counterparts, who would destroy it.", "Chaotic Evil" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", null, "DM", "dm" },
                    { "d4cef1d2-b6b5-4f0e-b252-68461fb7b4e9", null, "Admin", "admin" },
                    { "f57b92a3-d8e9-4b85-bd0f-0f4f9b1c527b", null, "Player", "player" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7bd95d37-7864-4a41-9002-9c40eba9d310", 0, "a5440aa9-ef53-42c9-9beb-b89c8029b6ef", "ShepardN7@Nrmdy.gov", false, false, null, null, null, "AQAAAAIAAYagAAAAEJMvp9/0cbAqVntA+UGX8T3t4iiOpUdtmjTfY9da2S1duZaPqvqAmXXsVPSGktNblw==", null, false, "f428e754-3c9b-45f9-829b-a5534b88c2f0", false, "ShepCmndr" },
                    { "8b0ba53c-ee98-4415-a5cb-bb249d8631e5", 0, "50bdf312-cb63-477a-97dc-4e9db221cbc9", "BleeMull@D20.Live", false, false, null, null, null, "AQAAAAIAAYagAAAAEHNgK3f0BVXEfyHZxKFLRtJ6QtVRgVYuQAoBdtRNxiRq+vfOphLziK3y4Lx5SxAC/g==", null, false, "d868993b-f167-49e0-a95b-b35cf28a5ceb", false, "BLeeM" },
                    { "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f", 0, "5ea9331d-0986-4c98-bb0c-6b455862c86c", "Clonch@mr.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEI0cKB1QEBwGQ63/hPO4ctFrYCZ/euhJ2Xnac3jg29/3GU/2sp0LboA6UBl0p725lg==", null, false, "b8d37e75-7aaf-4847-bb04-b5a0149d8105", false, "ClonchMr" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "ClassName", "HitDie" },
                values: new object[,]
                {
                    { 1, "Sorcerer", 8 },
                    { 2, "Wizard", 8 },
                    { 3, "Bard", 8 },
                    { 4, "Cleric", 8 },
                    { 5, "Druid", 8 },
                    { 6, "Monk", 8 },
                    { 7, "Rogue", 8 },
                    { 8, "Warlock", 8 },
                    { 9, "Fighter", 10 },
                    { 10, "Ranger", 10 },
                    { 11, "Paladin", 10 },
                    { 12, "Barbarian", 12 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ArmorClass", "CharacterId", "Damage", "ItemDescription", "ItemName", "ItemType", "Notes", "Weight" },
                values: new object[,]
                {
                    { 1, 12, null, null, "Made from tough but flexible leather, studded leather is reinforced with close-set rivets or spikes.", "Studded Leather", "Armor", null, 13.0 },
                    { 2, 13, null, null, "Made of interlocking metal rings, a chain shirt is worn between layers of clothing or leather. This armor offers modest protection to the wearer's upper body and allows the sound of the rings rubbing against one another to be muffled by outer layers.", "Chain Shirt", "Armor", null, 20.0 },
                    { 3, 16, null, null, "Made of interlocking metal rings, chain mail includes a layer of quilted fabric worn underneath the mail to prevent chafing and to cushion the impact of blows. The suit includes gauntlets.", "Chain Mail", "Armor", null, 55.0 },
                    { 4, 2, null, null, " A shield is made from wood or metal and is carried in one hand. Wielding a shield increases your Armor Class by 2. You can benefit from only one shield at a time. ", "Wooden Shield", "Shield", "+2 AC", 6.0 },
                    { 5, 2, null, null, "While holding this shield, you have resistance to damage from ranged weapon attacks.\r\n\r\nCurse. This shield is cursed. Attuning to it curses you until you are targeted by the remove curse spell or similar magic. Removing the shield fails to end the curse on you. Whenever a ranged weapon attack is made against a target within 10 feet of you, the curse causes you to become the target instead.\r\nShields. A shield is made from wood or metal and is carried in one hand. Wielding a shield increases your Armor Class by 2. You can benefit from only one shield at a time.\r\n", "Shield of Missile Attraction", "Shield", "+2 AC, Res to Range", 6.0 },
                    { 6, null, null, "1d4 Piercing", "Melee weapon (simple, dagger)\r\n\r\nFinesse. When making an attack with a finesse weapon, you use your choice of your Strength or Dexterity modifier for the attack and damage rolls. You must use the same modifier for both rolls.\r\nLight. A light weapon is small and easy to handle, making it ideal for use when fighting with two weapons.\r\nRange. A weapon that can be used to make a ranged attack has a range in parentheses after the ammunition or thrown property. The range lists two numbers. The first is the weapon's normal range in feet, and the second indicates the weapon's long range. When attacking a target beyond normal range, you have disadvantage on the attack roll. You can't attack a target beyond the weapon's long range. \r\nThrown. If a weapon has the thrown property, you can throw the weapon to make a ranged attack. If the weapon is a melee weapon, you use the same ability modifier for that attack roll and damage roll that you would use for a melee attack with the weapon. For example, if you throw a handaxe, you use your Strength, but if you throw a dagger, you can use either your Strength or your Dexterity, since the dagger has the finesse property.\r\n", "Dagger", "Weapon", "Light/Finesse", 1.0 },
                    { 7, null, null, "1d8/1d10 slashing", "Melee weapon (martial, longsword)\r\nVersatile. This weapon can be used with one or two hands. A damage value in parentheses appears with the property–the damage when the weapon is used with two hands to make a melee attack\r\n\r\nDamage Type: Slashing\r\nProperties: Versatile", "Longsword", "Weapon", "Martial/Versatile", 3.0 },
                    { 8, null, null, "1d8 slashing", "Melee weapon (martial, battleaxe)\r\nVersatile. This weapon can be used with one or two hands. A damage value in parentheses appears with the property- the damage when the weapon is used with two hands to make a melee attack. \r\n", "Battleaxe", "Weapon", "Martial/Versatile", 4.0 },
                    { 9, null, null, "1d8 piercing", "Ranged weapon (martial, longbow)\r\nAmmunition. You can use a weapon that has the Ammunition property to make a Ranged Attack only if you have Ammunition to fire from the weapon. Each time you Attack with the weapon, you expend one piece of Ammunition. Drawing the Ammunition from a quiver, case, or other container is part of the Attack. At the end of the battle, you can recover half your expended Ammunition by taking a minute to Search the Battlefield.\r\nIf you use a weapon that has the Ammunition property to make a melee Attack, you treat the weapon as an Improvised Weapon. A sling must be loaded to deal any damage when used in this way.\r\nHeavy. Small creatures have disadvantage on attack rolls with heavy weapons. A heavy weapon's size and bulk make it too large for a Small creature to use effectively.\r\nRange. A weapon that can be used to make a ranged attack has a range in parentheses after the ammunition or thrown property. The range lists two numbers. The first is the weapon’s normal range in feet, and the second indicates the weapon’s long range. When attacking a target beyond normal range, you have disadvantage on the attack roll. You can’t attack a target beyond the weapon’s long range.\r\nTwo-Handed. This weapon requires two hands when you attack with it.\r\n", "Longbow", "Weapon", "Martial/Heavy/Range", 2.0 },
                    { 10, null, null, null, "You regain hit points when you drink this potion. The number of hit points depends on the potion's rarity, as shown in the Potions of Healing table. Whatever its potency, the potion's red liquid glimmers when agitated.", "Potion of Healing", "Consumable", "2d4 + 2", 0.5 }
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Description", "SpeciesName", "Speed" },
                values: new object[,]
                {
                    { 1, "In the reckonings of most worlds, humans are the youngest of the common races, late to arrive on the world scene and short-lived in comparison to dwarves, elves, and dragons. Perhaps it is because of their shorter lives that they strive to achieve as much as they can in the years they are given. Or maybe they feel they have something to prove to the elder races, and that's why they build their mighty empires on the foundation of conquest and trade. Whatever drives them, humans are the innovators, the achievers, and the pioneers of the worlds.", "Human", 30 },
                    { 2, "Elves are a magical people of otherworldly grace, living in places of ethereal beauty, in the midst of ancient forests or in silvery spires glittering with faerie light, where soft music drifts through the air and gentle fragrances waft on the breeze. Elves love nature and magic, art and artistry, music and poetry.", "Elf", 30 },
                    { 3, "Kingdoms rich in ancient grandeur, halls carved into the roots of mountains, the echoing of picks and hammers in deep mines and blazing forges, a commitment to clan and tradition, and a burning hatred of goblins and orcs – these common threads unite all dwarves.", "Dwarf", 25 },
                    { 4, "When alliances between humans and orcs are sealed by marriages, half-orcs are born. Some half-orcs rise to become proud chiefs of orc tribes, their human blood giving them an edge over their full-blooded orc rivals. Some venture into the world to prove their worth among humans and other more civilized races. Many of these become adventurers, achieving greatness for their mighty deeds and notoriety for their barbaric customs and savage fury.", "Half-Orc", 30 },
                    { 5, "To be greeted with stares and whispers, to suffer violence and insult on the street, to see mistrust and fear in every eye: this is the lot of the tiefling. And to twist the knife, tieflings know that this is because a pact struck generations ago infused the essence of Asmodeus, overlord of the Nine Hells (and many of the other powerful devils serving under him) into their bloodline. Their appearance and their nature are not their fault but the result of an ancient sin, for which they and their children and their children's children will always be held accountable.", "Tiefling", 30 },
                    { 6, "Born of dragons, as their name proclaims, the dragonborn walk proudly through a world that greets them with fearful incomprehension. Shaped by draconic gods or the dragons themselves, dragonborn originally hatched from dragon eggs as a unique race, combining the best attributes of dragons and humanoids. Some dragonborn are faithful servants to true dragons, others form the ranks of soldiers in great wars, and still others find themselves adrift, with no clear calling in life.", "Dragonborn", 30 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d4cef1d2-b6b5-4f0e-b252-68461fb7b4e9", "7bd95d37-7864-4a41-9002-9c40eba9d310" },
                    { "d4cef1d2-b6b5-4f0e-b252-68461fb7b4e9", "8b0ba53c-ee98-4415-a5cb-bb249d8631e5" },
                    { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f" },
                    { "d4cef1d2-b6b5-4f0e-b252-68461fb7b4e9", "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f" },
                    { "f57b92a3-d8e9-4b85-bd0f-0f4f9b1c527b", "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f" }
                });

            migrationBuilder.InsertData(
                table: "ClassAbilities",
                columns: new[] { "Id", "AbilityId", "ClassId" },
                values: new object[,]
                {
                    { 1, 105, 1 },
                    { 2, 106, 1 },
                    { 3, 107, 1 },
                    { 4, 105, 2 },
                    { 5, 106, 2 },
                    { 6, 107, 2 },
                    { 7, 102, 3 },
                    { 8, 107, 3 },
                    { 9, 108, 3 },
                    { 10, 109, 3 },
                    { 11, 100, 4 },
                    { 12, 101, 4 },
                    { 13, 102, 4 },
                    { 14, 103, 4 },
                    { 15, 104, 4 },
                    { 16, 102, 5 },
                    { 17, 105, 5 },
                    { 18, 110, 5 },
                    { 19, 111, 5 },
                    { 20, 122, 5 },
                    { 21, 102, 6 },
                    { 22, 121, 6 },
                    { 23, 107, 6 },
                    { 24, 112, 7 },
                    { 25, 113, 7 },
                    { 26, 114, 8 },
                    { 27, 115, 8 },
                    { 28, 116, 8 },
                    { 29, 117, 9 },
                    { 30, 118, 9 },
                    { 31, 119, 9 },
                    { 32, 120, 10 },
                    { 33, 121, 10 },
                    { 34, 122, 10 },
                    { 35, 110, 10 },
                    { 36, 111, 10 },
                    { 37, 123, 11 },
                    { 38, 124, 11 },
                    { 39, 125, 11 },
                    { 40, 126, 12 },
                    { 41, 127, 12 },
                    { 42, 128, 12 }
                });

            migrationBuilder.InsertData(
                table: "SubClasses",
                columns: new[] { "Id", "ClassId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Your innate magic comes from draconic magic that was mingled with your blood or that of your ancestors. Most often, sorcerers with this origin trace their descent back to a mighty sorcerer of ancient times who made a bargain with a dragon or who might even have claimed a dragon parent. Some of these bloodlines are well established in the world, but most are obscure. Any given sorcerer could be the first of a new bloodline, as a result of a pact or some other exceptional circumstance.", "Draconic Bloodline" },
                    { 2, 1, "Your innate magic comes from the wild forces of chaos that underlie the order of creation. You might have endured exposure to some form of raw magic, perhaps through a planar portal leading to Limbo, the Elemental Planes, or the mysterious Far Realm. Perhaps you were blessed by a powerful fey creature or marked by a demon. Or your magic could be a fluke of your birth, with no apparent cause or reason. However it came to be, this chaotic magic churns within you, waiting for any outlet.", "Wild Magic" },
                    { 3, 1, "Your innate magic comes from the power of elemental air. Many with this power can trace their magic back to a near-death experience caused by the Great Rain, but perhaps you were born during a howling gale so powerful that folk still tell stories of it, or your lineage might include the influence of potent air creatures such as vaati or djinn. Whatever the case, the magic of the storm permeates your being.\r\nStorm sorcerers are invaluable members of a ship's crew. Their magic allows them to exert control over wind and weather in their immediate area. Their abilities also prove useful in repelling attacks by sahuagin, pirates, and other waterborne threats.\r\n", "Storm Sorcery" },
                    { 4, 2, "The School of Abjuration emphasizes magic that blocks, banishes, or protects. Detractors of this school say that its tradition is about denial, negation rather than positive assertion. You understand, however, that ending harmful effects, protecting the weak, and banishing evil influences is anything but a philosophical void. It is a proud and respected vocation.\r\nCalled abjurers, members of this school are sought when baleful spirits require exorcism, when important locations must be guarded against magical spying, and when portals to other planes of existence must be closed.\r\n", "Abjuration" },
                    { 5, 2, "The counsel of a diviner is sought by royalty and commoners alike, for all seek a clearer understanding of the past, present, and future. As a diviner, you strive to part the veils of space, time, and consciousness so that you can see clearly. You work to master spells of discernment, remote viewing, supernatural knowledge, and foresight.", "Divination" },
                    { 6, 2, "As a conjurer, you favor spells that produce objects and creatures out of thin air. You can conjure billowing clouds of killing fog or summon creatures from elsewhere to fight on your behalf. As your mastery grows, you learn spells of transportation and can teleport yourself across vast distances, even to other planes of existence, in an instant.", "Conjuration" },
                    { 7, 3, "Bards of the College of Lore know something about most things, collecting bits of knowledge from sources as diverse as scholarly tomes and peasant tales. Whether singing folk ballads in taverns or elaborate compositions in royal courts, these bards use their gifts to hold audiences spellbound. When the applause dies down, the audience members might find themselves questioning everything they held to be true, from their faith in the priesthood of the local temple to their loyalty to the king.\r\nThe loyalty of these bards lies in the pursuit of beauty and truth, not in fealty to a monarch or following the tenets of a deity. A noble who keeps such a bard as a herald or advisor knows that the bard would rather be honest than politic.\r\nThe college's members gather in libraries and sometimes in actual colleges, complete with classrooms and dormitories, to share their lore with one another. They also meet at festivals or affairs of state, where they can expose corruption, unravel lies, and poke fun at self-important figures of authority.\r\n", "College of Lore" },
                    { 8, 3, " Bards of the College of Valor are daring skalds whose tales keep alive the memory of the great heroes of the past, and thereby inspire a new generation of heroes. These bards gather in mead halls or around great bonfires to sing the deeds of the mighty, both past and present. They travel the land to witness great events firsthand and to ensure that the memory of those events doesn't pass from the world. With their songs, they inspire others to reach the same heights of accomplishment as the heroes of old", "College of Valor" },
                    { 9, 3, " Bards of the College of Swords are called blades, and they entertain through daring feats of weapon prowess. Blades perform stunts such as sword swallowing, knife throwing and juggling, and mock combats. Though they use their weapons to entertain, they are also highly trained and skilled warriors in their own right.\r\nTheir talent with weapons inspires many blades to lead double lives. One blade might use a circus troupe as cover for nefarious deeds such as assassination, robbery, and blackmail. Other blades strike at the wicked, bringing justice to bear against the cruel and powerful. Most troupes are happy to accept a blade’s talent for the excitement it adds to a performance, but few entertainers fully trust a blade in their ranks.\r\nBlades who abandon their lives as entertainers have often run into trouble that makes maintaining their secret activities impossible. A blade caught stealing or engaging in vigilante justice is too great a liability for most troupes. With their weapon skills and magic, these blades either take up work as enforcers for thieves’ guilds or strike out on their own as adventurers.\r\n", "College of Swords" },
                    { 10, 4, "The Life domain focuses on the vibrant positive energy – one of the fundamental forces of the universe – that sustains all life. The gods of life promote vitality and health through healing the sick and wounded, caring for those in need, and driving away the forces of death and undeath. Almost any non-evil deity can claim influence over this domain, particularly agricultural deities (such as Chauntea, Arawai, and Demeter), sun gods (such as Lathander, Pelor, and Re-Horakhty), gods of healing or endurance (such as Ilmater, Mishakal, Apollo, and Diancecht), and gods of home and community (such as Hestia, Hathor, and Boldrci).", "Life Domain" },
                    { 11, 4, "War has many manifestations. It can make heroes of ordinary people. It can be desperate and horrific, with acts of cruelty and cowardice eclipsing instances of excellence and courage. In either case, the gods of war watch over warriors and reward them for their great deeds. The clerics of such gods excel in battle, inspiring others to fight the good fight or offering acts of violence as prayers. Gods of war include champions of honor and chivalry (such as Torm, Heironeous, and Kiri-Jolith) as well as gods of destruction and pillage (such as Erythnul, the Fury, Gruumsh, and Ares) and gods of conquest and domination (such as Bane, Hextor, and Maglubiyet). Other war gods (such as Tempus, Nike, and Nuada) take a more neutral stance, promoting war in all its manifestations and supporting warriors in any circumstance.", "War Domain" },
                    { 12, 4, "The twilit transition from light into darkness often brings calm and even joy, as the day's labors end and the hours of rest begin. The darkness can also bring terrors, but the gods of twilight guard against the horrors of the night.\r\nClerics who serve these deities-examples of which appear on the Twilight Deities table-bring comfort to those who seek rest and protect them by venturing into the encroaching darkness to ensure that the dark is a comfort, not a terror.", "Twilight Domain" },
                    { 13, 5, "Druids of the Circle of the Moon are fierce guardians of the wilds. Their order gathers under the full moon to share news and trade warnings. They haunt the deepest parts of the wilderness, where they might go for weeks on end before crossing paths with another humanoid creature, let alone another druid.\r\nChangeable as the moon, a druid of this circle might prowl as a great cat one night, soar over the treetops as an eagle the next day, and crash through the undergrowth in bear form to drive off a trespassing monster. The wild is in the druid's blood.\r\n", "Circle of the Moon" },
                    { 14, 5, "The Circle of Stars allows druids to draw on the power of starlight. These druids have tracked heavenly patterns since time immemorial, discovering secrets hidden amid the constellations. By revealing and understanding these secrets, the Circle of the Stars seeks to harness the powers of the cosmos.\r\nMany druids of this circle keep records of the constellations and the stars' effects on the world. Some groups document these observations at megalithic sites, which serve as enigmatic libraries of lore. These repositories might take the form of stone circles, pyramids, petroglyphs, and underground temples; any construction durable enough to protect the circle's sacred knowledge even against a great cataclysm.\r\n", "Circle of Stars" },
                    { 15, 5, "The Circle of the Land is made up of mystics and sages who safeguard ancient knowledge and rites through a vast oral tradition. These druids meet within sacred circles of trees or standing stones to whisper primal secrets in Druidic. The circle's wisest members preside as the chief priests of communities that hold to the Old Faith and serve as advisors to the rulers of those folk. As a member of this circle, your magic is influenced by the land where you were initiated into the circle's mysterious rites.", "Circle of the Land" },
                    { 16, 6, "Monks of the Way of the Open Hand are the ultimate masters of martial arts combat, whether armed or unarmed. They learn techniques to push and trip their opponents, manipulate ki to heal damage to their bodies, and practice advanced meditation that can protect them from harm.\r\n", "Way of the Open Hand" },
                    { 17, 6, "You follow a monastic tradition that teaches you to harness the elements. When you focus your ki, you can align yourself with the forces of creation and bend the four elements to your will, using them as an extension of your body. Some members of this tradition dedicate themselves to a single element, but others weave the elements together.\r\nMany monks of this tradition tattoo their bodies with representations of their ki powers, commonly imagined as coiling dragons, but also as phoenixes, fish, plants, mountains, and cresting waves.\r\n", "Way of the 4 Elements" },
                    { 18, 6, "The Way of the Drunken Master teaches its students to move with the jerky, unpredictable movements of a drunkard. A drunken master sways, tottering on unsteady feet, to present what seems like an incompetent combatant who proves frustrating to engage. The drunken master’s erratic stumbles conceal a carefully executed dance of blocks, parries, advances, attacks, and retreats.\r\nA drunken master often enjoys playing the fool to bring gladness to the despondent or to demonstrate humility to the arrogant, but when battle is joined, the drunken master can be a maddening, masterful foe.\r\n", "Way of the Drunken Master" },
                    { 19, 7, "You focus your training on the grim art of death. Those who adhere to this archetype are diverse: hired killers, spies, bounty hunters, and even specially anointed priests trained to exterminate the enemies of their deity. Stealth, poison, and disguise help you eliminate your foes with deadly efficiency.", "Assassin" },
                    { 20, 7, "Some rogues enhance their fine-honed skills of stealth and agility with magic, learning tricks of enchantment and illusion. These rogues include pickpockets and burglars, but also pranksters, mischief-makers, and a significant number of adventurers.", "Arcane Trickster" },
                    { 21, 7, "You focus your training on the art of the blade, relying on speed, elegance, and charm in equal parts. While some warriors are brutes clad in heavy armor, your method of fighting looks almost like a performance. Duelists and pirates typically belong to this archetype.\r\nA Swashbuckler excels in single combat, and can fight with two weapons while safely darting away from an opponent.\r\n", "Swashbuckler" },
                    { 22, 8, "You have made a pact with a fiend from the lower planes of existence, a being whose aims are evil, even if you strive against those aims. Such beings desire the corruption or destruction of all things, ultimately including you. Fiends powerful enough to forge a pact include demon lords such as Demogorgon, Orcus, Fraz'Urb-luu, and Baphomet; archdevils such as Asmodeus, Dispater, Mephistopheles, and Belial; pit fiends and balors that are especially mighty; and ultroloths and other lords of the yugoloths.", "Pact of the Fiend" },
                    { 23, 8, "Your patron is a mysterious entity whose nature is utterly foreign to the fabric of reality. It might come from the Far Realm, the space beyond reality, or it could be one of the elder gods known only in legends. Its motives are incomprehensible to mortals, and its knowledge so immense and ancient that even the greatest libraries pale in comparison to the vast secrets it holds. The Great Old One might be unaware of your existence or entirely indifferent to you, but the secrets you have learned allow you to draw your magic from it.\r\nEntities of this type include Ghaunadar, called That Which Lurks; Tharizdun, the Chained God; Dendar, the Night Serpent; Zargon, the Returner; Great Cthulhu; and other unfathomable beings.\r\n", "PAct of the Great Old One" },
                    { 24, 8, "You have made your pact with a mysterious entity from the Shadowfell – a force that manifests in sentient magic weapons carved from the stuff of shadow. The mighty sword Blackrazor is the most notable of these weapons, which have been spread across the multiverse over the ages. The shadowy force behind these weapons can offer power to warlocks who form pacts with it. Many hexblade warlocks create weapons that emulate those formed in the Shadowfell. Others forgo such arms, content to weave the dark magic of that plane into their spellcasting.\r\nBecause the Raven Queen is known to have forged the first of these weapons, many sages speculate that she and the force are one and that the weapons, along with hexblade warlocks, are tools she uses to manipulate events on the Material Plane to her inscrutable ends.\r\n", "Pact of the Hexblade" },
                    { 25, 9, "Those who emulate the archetypal Battle Master employ martial techniques passed down through generations. To a Battle Master, combat is an academic field, sometimes including subjects beyond battle such as weaponsmithing and calligraphy. Not every fighter absorbs the lessons of history, theory, and artistry that are reflected in the Battle Master archetype, but those who do are well-rounded fighters of great skill and knowledge.", "Battle Master" },
                    { 26, 9, "The archetypal Champion focuses on the development of raw physical power honed to deadly perfection. Those who model themselves on this archetype combine rigorous training with physical excellence to deal devastating blows.", "Champion" },
                    { 27, 9, "The archetypal Eldritch Knight combines the martial mastery common to all fighters with a careful study of magic. Eldritch Knights use magical techniques similar to those practiced by wizards. They focus their study on two of the eight schools of magic: abjuration and evocation. Abjuration spells grant an Eldritch Knight additional protection in battle, and evocation spells deal damage to many foes at once, extending the fighter's reach in combat. These knights learn a comparatively small number of spells, committing them to memory instead of keeping them in a spellbook.", "Eldritch Knight" },
                    { 28, 10, "The Beast Master archetype embodies a friendship between the civilized races and the beasts of the world. United in focus, beast and ranger work as one to fight the monstrous foes that threaten civilization and the wilderness alike. Emulating the Beast Master archetype means committing yourself to this ideal, working in partnership with an animal as its companion and friend.", "Beast Master Conclave" },
                    { 29, 10, "Gloom stalkers are at home in the darkest places: deep under the earth, in gloomy alleyways, in primeval forests, and wherever else the light dims. Most folk enter such places with trepidation, but a gloom stalker ventures boldly into the darkness, seeking to ambush threats before they can reach the broader world. Such rangers are often found in the Underdark, but they will go any place where evil lurks in the shadows.", "Gloom Stalker Conclave" },
                    { 30, 10, "Horizon walkers guard the world against threats that originate from other planes or that seek to ravage the mortal realm with otherworldly magic. They seek out planar portals and keep watch over them, venturing to the Inner Planes and the Outer Planes as needed to pursue their foes. These rangers are also friends to any forces in the multiverse – especially benevolent dragons, fey, and elementals – that work to preserve life and the order of the planes.", "Horizon Walker Conclave" },
                    { 31, 11, "The Oath of Conquest calls to paladins who seek glory in battle and the subjugation of their enemies. It isn’t enough for these paladins to establish order. They must crush the forces of chaos. Sometimes called knight tyrants or iron mongers, those who swear this oath gather into grim orders that serve gods or philosophies of war and well-ordered might.\r\nSome of these paladins go so far as to consort with the powers of the Nine Hells, valuing the rule of law over the balm of mercy. The archdevil Bel, warlord of Avernus, counts many of these paladins – called hell knights – as his most ardent supporters. Hell knights cover their armor with trophies taken from fallen enemies, a grim warning to any who dare oppose them and the decrees of their lords. These knights are often most fiercely resisted by other paladins of this oath, who believe that the hell knights have wandered too far into darkness.\r\n", "Oath of Conquest" },
                    { 32, 11, "The Oath of Devotion binds a paladin to the loftiest ideals of justice, virtue, and order. Sometimes called cavaliers, white knights, or holy warriors, these paladins meet the ideal of the knight in shining armor, acting with honor in pursuit of justice and the greater good. They hold themselves to the highest standards of conduct, and some, for better or worse, hold the rest of the world to the same standards. Many who swear this oath are devoted to gods of law and good and use their gods' tenets as the measure of their devotion. They hold angels – the perfect servants of good – as their ideals, and incorporate images of angelic wings into their helmets or coats of arms.", "Oath of Devotion" },
                    { 33, 11, "The Oath of Vengeance is a solemn commitment to punish those who have committed a grievous sin. When evil forces slaughter helpless villagers, when an entire people turns against the will of the gods, when a thieves' guild grows too violent and powerful, when a dragon rampages through the countryside – at times like these, paladins arise and swear an Oath of Vengeance to set right that which has gone wrong. To these paladins – sometimes called avengers or dark knights – their own purity is not as important as delivering justice.", "Oath of Vengeance" },
                    { 34, 12, "For some barbarians, rage is a means to an end – that end being violence. The Path of the Berserker is a path of untrammeled fury, slick with blood. As you enter the berserker's rage, you thrill in the chaos of battle, heedless of your own health or well-being.", "Path of the Berzerker" },
                    { 35, 12, "Typical barbarians harbor a fury that dwells within. Their rage grants them superior strength, durability, and speed. Barbarians who follow the Path of the Storm Herald learn instead to transform their rage into a mantle of primal magic that swirls around them. When in a fury, a barbarian of this path taps into nature to create powerful, magical effects.\r\nStorm heralds are typically elite champions who train alongside druids, rangers, and others sworn to protect the natural realm. Other storm heralds hone their craft in elite lodges founded in regions wracked by storms, in the frozen reaches at the world’s end, or deep in the hottest deserts.\r\n", "Path of the Storm Herald" },
                    { 36, 12, "Many places in the multiverse abound with beauty, intense emotion, and rampant magic; the Feywild, the Upper Planes, and other realms of supernatural power radiate with such forces and can profoundly influence people. As folk of deep feeling, barbarians are especially susceptible to these wild influences, with some barbarians being transformed by the magic. These magic-suffused barbarians walk the Path of Wild Magic. Elf, tiefling, aasimar, and genasi barbarians often seek this path, eager to manifest the otherworldly magic of their ancestors.", "Path of Wild Magic" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "FirstName", "IdentityUserId", "LastName" },
                values: new object[,]
                {
                    { 1, "Matthew", "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f", "Clonch" },
                    { 2, "Brennan", "8b0ba53c-ee98-4415-a5cb-bb249d8631e5", "Lee Mulligan" },
                    { 3, "John", "7bd95d37-7864-4a41-9002-9c40eba9d310", "Shephard" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Id", "CampaignDescription", "CampaignName", "CampaignPicUrl", "EndDate", "LevelRange", "OwnerId", "StartDate" },
                values: new object[] { 1, "Tyranny of Dragons deals with the rise of evil dragons and their attempt to free Tiamat from the Nine Hells.", "Tyranny of Dragons", "https://upload.wikimedia.org/wikipedia/en/3/3d/Tyranny_of_Dragons%2C_2023_rerelease_print_cover.jpg", null, "1 - 5", 1, new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Age", "AlignmentId", "Backstory", "CharacterPicUrl", "Charisma", "ClassId", "Constitution", "Dexterity", "Faith", "Gender", "Height", "HitPoints", "Intelligence", "Level", "Name", "SpeciesId", "Strength", "SubClassId", "UserId", "Weight", "Wisdom" },
                values: new object[,]
                {
                    { 1, 31, 2, "A lone cleric worshiping the great Jimmy Buffet travels between different Margaritavilles spreading the good word.", "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/e391c797-fae0-4286-ac94-2b42df8c8c71/dfxcq7f-df5e4a49-7fae-4f62-b23a-dbfe0fd34dd9.png/v1/fill/w_1280,h_1600,q_80,strp/thraxis_kartum__half_elf_cleric_by_hhahkk_dfxcq7f-fullview.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9MTYwMCIsInBhdGgiOiJcL2ZcL2UzOTFjNzk3LWZhZTAtNDI4Ni1hYzk0LTJiNDJkZjhjOGM3MVwvZGZ4Y3E3Zi1kZjVlNGE0OS03ZmFlLTRmNjItYjIzYS1kYmZlMGZkMzRkZDkucG5nIiwid2lkdGgiOiI8PTEyODAifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6aW1hZ2Uub3BlcmF0aW9ucyJdfQ.wu5X2HNmshCLKAs3ofo9MlPwpvz9ADNhPxzPlmdVioI", 11, 4, 14, 12, "Jimmy Buffet", "Male", "6'2", 0, 8, 1, "Marty Dickson", 2, 10, null, 2, "200", 17 },
                    { 2, 25, 6, "A monk with a love for fists and a passion for shenanigans. Growing up in the eastlands was tough, but it made me into the dwarf I am today.", "data:image/jpeg;base64,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", 8, 6, 14, 14, "Fists", "Male", "4'0", 0, 12, 1, "Garfunkle", 3, 10, null, 2, "180", 17 },
                    { 3, 20, 3, "A bard with a love for Lily and a passion for song and dance, east bound and down, loaded up and trucking.", "data:image/jpeg;base64,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", 17, 3, 16, 14, "Lily", "Male", "3'1", 0, 10, 1, "F.R.I.E.N.D", 6, 8, null, 3, "120", 8 },
                    { 4, 32, 5, "Peaking in highschool as the starting QB of the Bloodrush team was a dream, now I'm just adventuring and swinging my axe.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRe4TmKKgKqVw8qmHlOBxArLboPnrZyWl0zbw&s", 10, 12, 16, 12, "Axe", "Male", "6'10", 0, 8, 1, "Hasbeen", 4, 17, null, 3, "295", 11 }
                });

            migrationBuilder.InsertData(
                table: "CharacterAbilities",
                columns: new[] { "AbilityId", "CharacterId" },
                values: new object[,]
                {
                    { 100, 1 },
                    { 101, 1 },
                    { 102, 2 },
                    { 107, 2 },
                    { 108, 3 },
                    { 109, 3 },
                    { 126, 4 },
                    { 127, 4 }
                });

            migrationBuilder.InsertData(
                table: "CharacterItems",
                columns: new[] { "Id", "CharacterId", "IsEquipped", "ItemId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, false, 2, 1 },
                    { 2, 1, false, 4, 1 },
                    { 3, 1, false, 7, 1 },
                    { 4, 2, false, 1, 1 },
                    { 5, 3, false, 1, 1 },
                    { 6, 4, false, 8, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_CharacterId",
                table: "Abilities",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacter_CharactersId",
                table: "CampaignCharacter",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignLogs_CampaignId",
                table: "CampaignLogs",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_OwnerId",
                table: "Campaigns",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAbilities_AbilityId",
                table: "CharacterAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterCampaigns_CampaignId",
                table: "CharacterCampaigns",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterCampaigns_CharacterId",
                table: "CharacterCampaigns",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterItems_CharacterId",
                table: "CharacterItems",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterItems_ItemId",
                table: "CharacterItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AlignmentId",
                table: "Characters",
                column: "AlignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ClassId",
                table: "Characters",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SpeciesId",
                table: "Characters",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SubClassId",
                table: "Characters",
                column: "SubClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbilities_AbilityId",
                table: "ClassAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbilities_ClassId",
                table: "ClassAbilities",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_CampaignId",
                table: "Invitations",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_RecipientId",
                table: "Invitations",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_SenderId",
                table: "Invitations",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CharacterId",
                table: "Items",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SubClasses_ClassId",
                table: "SubClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CampaignCharacter");

            migrationBuilder.DropTable(
                name: "CampaignLogs");

            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "CharacterCampaigns");

            migrationBuilder.DropTable(
                name: "CharacterItems");

            migrationBuilder.DropTable(
                name: "ClassAbilities");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Alignments");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "SubClasses");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
