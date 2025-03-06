using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampaignNexus.Migrations
{
    /// <inheritdoc />
    public partial class FixedErrorCalculatingHealth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7bd95d37-7864-4a41-9002-9c40eba9d310",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f65e0092-31b6-414a-ac1b-48eafbfac7d9", "AQAAAAIAAYagAAAAEHkYNoqUKghLFoSmzAQOeUWdl7KXOwN/vi7619pCpTiJJbF/CEwsyPdAmwZgY0iO2g==", "6c484650-1515-4406-9641-bbb68807089b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b0ba53c-ee98-4415-a5cb-bb249d8631e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c773f6a-e8b9-41e6-9268-282599c4297b", "AQAAAAIAAYagAAAAEKpgRVhtcFztjWDQSt+GaSeGJkgpfv+1G/QgDLmqNozCC2fMmM/a0DM80owTkRy2Vw==", "6a4bebdb-d910-4173-b3aa-efb2e58384f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8464ee5-20a1-44e6-bab4-c5ebfd6786a7", "AQAAAAIAAYagAAAAENWYB/dhspi4aiuHZeDEEaarr+dMV2l7ZEUUJ7H/G0Ffhsr0gy7ncHYzx/jEGftOCw==", "9d9bf192-2b7e-4f23-97fc-b440e940bcd8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
