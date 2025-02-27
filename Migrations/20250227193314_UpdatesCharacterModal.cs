using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampaignNexus.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesCharacterModal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7bd95d37-7864-4a41-9002-9c40eba9d310",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b301323a-8f76-4c1a-b044-640592abbf4e", "AQAAAAIAAYagAAAAEFhWzXxEfJ7d2KmNyum5aVTuGj5M4S7N6zlY5wlNJYy8rv2hThERmTARv95RF2LZLQ==", "b7877698-94f1-4f14-a109-44a8e7a14129" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b0ba53c-ee98-4415-a5cb-bb249d8631e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "971e062d-b0e6-4c32-9e91-b9b8ad4c72d8", "AQAAAAIAAYagAAAAEGLHH4K9IOd5EAEb08TRD8sZMjZBtU6YLpzaVchOe+rCumJzfjJtprvZJ2Syg1AmjA==", "78c23153-db1b-4199-a2ed-ed29c9bff850" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f92dd099-4573-4dcb-a3fe-e7d367850aaf", "AQAAAAIAAYagAAAAEA+UaFD9a5c9+hDQQBQ4DTBzBiYHy37SLVW25v1OFGsYlHP0HwK/UI0DWBw0Pa4rLg==", "79aca043-d32f-43b1-83b5-fb8ce00b89cd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
