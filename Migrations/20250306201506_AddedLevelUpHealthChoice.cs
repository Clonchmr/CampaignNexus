using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampaignNexus.Migrations
{
    /// <inheritdoc />
    public partial class AddedLevelUpHealthChoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HitPoints",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RollForHp",
                table: "Characters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7bd95d37-7864-4a41-9002-9c40eba9d310",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8e61978-2502-457a-b37c-efde607e09b4", "AQAAAAIAAYagAAAAEOVJmKLNT1KlRyceCQflHNL+yjZb4XDHHLdO4nVtbhETr3mK5avrZ6CoLmDPN5RFgA==", "bce8efe2-57fe-4de9-b820-8b31d62ba7ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b0ba53c-ee98-4415-a5cb-bb249d8631e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1843c90b-27b8-44ae-a606-c229b6360d29", "AQAAAAIAAYagAAAAEEkZfsowq1mEFmTvMWjGRkiJPcUbyzdTWXoPWWYPx8QnlkVhpDhNQ3PmUi/5FeeAYw==", "1c2b8be6-13c8-4f05-8557-4b336bb69948" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72f72915-3501-4766-ba29-af278cb6046f", "AQAAAAIAAYagAAAAEDXHIATrTpbwDREW5Dro1PC0wHEpyQCycBlnRdYSTXt3LkjxhXdeWJmjGXED56ZkbQ==", "41f59fcb-9a81-4bb9-9d2a-0e3f9eb285f7" });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HitPoints", "RollForHp" },
                values: new object[] { 0, false });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HitPoints", "RollForHp" },
                values: new object[] { 0, false });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HitPoints", "RollForHp" },
                values: new object[] { 0, false });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HitPoints", "RollForHp" },
                values: new object[] { 0, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HitPoints",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RollForHp",
                table: "Characters");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7bd95d37-7864-4a41-9002-9c40eba9d310",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "444a6523-55ac-4e60-899f-847e52ca713f", "AQAAAAIAAYagAAAAEGfMePHuXF/hf5qFDUT9ist1ChinSNUyiOt3ZS3PUqRncgF2XrigSZl/kpCo+yhKVA==", "ee48ee2d-e7a9-4d7a-b987-20244cf8124e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b0ba53c-ee98-4415-a5cb-bb249d8631e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "327fbf5c-c5c9-4604-9b95-ca1f9c6805d2", "AQAAAAIAAYagAAAAEIyVYD1Wi7VfwJDKxLLnrGbAKQ9Tw6sCMljBNyYpIcm4yRim4uNyPTWMqWl7CTDTZQ==", "88b34c4c-1490-4fab-ac69-614e4a84792f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21b123e3-7a99-4bc4-b570-a882522ef694", "AQAAAAIAAYagAAAAEEVx0igJxhC1FJmYfJfqP/6mowuzyYTXO72uyIS5cTG5/hheIT87zYwDpPujUSYDIQ==", "aca082ef-f3d0-4a68-8744-3a346c557338" });
        }
    }
}
