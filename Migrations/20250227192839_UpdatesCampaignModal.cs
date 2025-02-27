using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampaignNexus.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesCampaignModal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignCharacter");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Campaigns",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7bd95d37-7864-4a41-9002-9c40eba9d310",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b3da5dd-3fd6-4a96-8fc2-5442cc26d2c3", "AQAAAAIAAYagAAAAEKFfXtm/p9KivH4tGhBN1FgLq53xf+etMvAwZjJ0VWKa5u2Lx+kP01T39v+Nc+kAmQ==", "47195da9-671b-4258-8fe8-89c19de93f82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b0ba53c-ee98-4415-a5cb-bb249d8631e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f25e1546-972b-4750-9dc5-3878c3864b25", "AQAAAAIAAYagAAAAEI8bDQ2OG2hWhfS7YNtI+ezYbZ+apxAqMGFXNWt/LhGiSCqJNMTCAe6Ie0LnkRkmfQ==", "ed2c35a7-0a09-4d98-bfc3-6af494f25951" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "977f6027-4b76-4ce0-9b45-97b70f4cd01d", "AQAAAAIAAYagAAAAEPV9ZzOkoFOYYOx+UOb2MvZjlvmrtSG2Mp45gZn1HVZppxnYD6u/NzU/nEML9mgKcQ==", "a007f28f-b1c5-4457-a10d-164091e777c8" });

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1,
                column: "CharacterId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CharacterId",
                table: "Campaigns",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Characters_CharacterId",
                table: "Campaigns",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Characters_CharacterId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_CharacterId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Campaigns");

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7bd95d37-7864-4a41-9002-9c40eba9d310",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5440aa9-ef53-42c9-9beb-b89c8029b6ef", "AQAAAAIAAYagAAAAEJMvp9/0cbAqVntA+UGX8T3t4iiOpUdtmjTfY9da2S1duZaPqvqAmXXsVPSGktNblw==", "f428e754-3c9b-45f9-829b-a5534b88c2f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b0ba53c-ee98-4415-a5cb-bb249d8631e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50bdf312-cb63-477a-97dc-4e9db221cbc9", "AQAAAAIAAYagAAAAEHNgK3f0BVXEfyHZxKFLRtJ6QtVRgVYuQAoBdtRNxiRq+vfOphLziK3y4Lx5SxAC/g==", "d868993b-f167-49e0-a95b-b35cf28a5ceb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ea9331d-0986-4c98-bb0c-6b455862c86c", "AQAAAAIAAYagAAAAEI0cKB1QEBwGQ63/hPO4ctFrYCZ/euhJ2Xnac3jg29/3GU/2sp0LboA6UBl0p725lg==", "b8d37e75-7aaf-4847-bb04-b5a0149d8105" });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCharacter_CharactersId",
                table: "CampaignCharacter",
                column: "CharactersId");
        }
    }
}
