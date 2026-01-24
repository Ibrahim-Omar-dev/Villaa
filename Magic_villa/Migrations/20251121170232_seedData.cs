using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Magic_villa.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillaNumbers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "SquareFeet", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Pool, WiFi, Breakfast", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A luxurious villa with stunning views.", "https://example.com/royal-villa.jpg", "Royal Villa", 4, 250.0, "2000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Private Pool, Gym, WiFi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Spacious villa with a private pool.", "https://example.com/premium-pool-villa.jpg", "Premium Pool Villa", 5, 300.0, "2500", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "VillaNumbers",
                columns: new[] { "VillaNum", "CreateAt", "SpecialDetails", "UpdateAt", "VillaId" },
                values: new object[,]
                {
                    { 101, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ocean view, 2 bedrooms", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 102, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mountain view, 3 bedrooms", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers");

            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "VillaNum",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "VillaNumbers",
                keyColumn: "VillaNum",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillaNumbers");
        }
    }
}
