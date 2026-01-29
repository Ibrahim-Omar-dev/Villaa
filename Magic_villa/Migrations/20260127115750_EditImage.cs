using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Magic_villa.Migrations
{
    /// <inheritdoc />
    public partial class EditImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://dotnetmastery.com/bluevillaimage/villa1.jpg");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://dotnetmastery.com/bluevillaimage/villa2.jpg");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "SquareFeet", "UpdatedDate" },
                values: new object[,]
                {
                    { 3, "Beach Access, Jacuzzi, WiFi, Chef Service", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Elegant beachfront villa with panoramic ocean views.", "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800", "Diamond Villa", 6, 450.0, "3200", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Infinity Pool, BBQ Area, WiFi, Parking", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Modern villa with infinity pool and mountain backdrop.", "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800", "Sunset Paradise Villa", 5, 380.0, "2800", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Garden, Outdoor Dining, WiFi, Air Conditioning", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tranquil villa surrounded by lush tropical gardens.", "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800", "Garden Oasis Villa", 4, 320.0, "2200", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://example.com/royal-villa.jpg");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://example.com/premium-pool-villa.jpg");
        }
    }
}
