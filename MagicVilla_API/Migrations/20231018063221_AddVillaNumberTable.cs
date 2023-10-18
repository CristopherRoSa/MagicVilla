using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    villaNum = table.Column<int>(type: "int", nullable: false),
                    villaID = table.Column<int>(type: "int", nullable: false),
                    specialDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.villaNum);
                    table.ForeignKey(
                        name: "FK_VillaNumbers_Villas_villaID",
                        column: x => x.villaID,
                        principalTable: "Villas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "creationDate", "updateDate" },
                values: new object[] { new DateTime(2023, 10, 18, 0, 32, 21, 82, DateTimeKind.Local).AddTicks(684), new DateTime(2023, 10, 18, 0, 32, 21, 82, DateTimeKind.Local).AddTicks(695) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "creationDate", "updateDate" },
                values: new object[] { new DateTime(2023, 10, 18, 0, 32, 21, 82, DateTimeKind.Local).AddTicks(699), new DateTime(2023, 10, 18, 0, 32, 21, 82, DateTimeKind.Local).AddTicks(701) });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_villaID",
                table: "VillaNumbers",
                column: "villaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "creationDate", "updateDate" },
                values: new object[] { new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6962), new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6980) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "creationDate", "updateDate" },
                values: new object[] { new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6987), new DateTime(2023, 10, 15, 21, 11, 47, 280, DateTimeKind.Local).AddTicks(6993) });
        }
    }
}
