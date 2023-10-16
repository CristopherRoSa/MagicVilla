using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class FeedTableVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "id", "amenity", "creationDate", "detail", "fee", "name", "occupants", "squareMeter", "updateDate", "urlImage" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6962), "Detalle de la villa...", 200.0, "Villa Real", 5, 50, new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6980), "" },
                    { 2, "", new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6987), "Detalle de la villa...", 150.0, "Premium vista a la piscina", 4, 40, new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6993), "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
