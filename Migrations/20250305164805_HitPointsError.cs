using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampaignNexus.Migrations
{
    /// <inheritdoc />
    public partial class HitPointsError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HitPoints",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HitPoints",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "HitPoints",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "HitPoints",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "HitPoints",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 4,
                column: "HitPoints",
                value: 0);
        }
    }
}
