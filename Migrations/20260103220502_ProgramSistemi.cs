using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YksKoclukSistemi.Migrations
{
    /// <inheritdoc />
    public partial class ProgramSistemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gun",
                table: "Odevler",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HaftalikProgramId",
                table: "Odevler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Programlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    HaftaNumarasi = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programlar_Kullanicilar_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odevler_HaftalikProgramId",
                table: "Odevler",
                column: "HaftalikProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programlar_OgrenciId",
                table: "Programlar",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odevler_Programlar_HaftalikProgramId",
                table: "Odevler",
                column: "HaftalikProgramId",
                principalTable: "Programlar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odevler_Programlar_HaftalikProgramId",
                table: "Odevler");

            migrationBuilder.DropTable(
                name: "Programlar");

            migrationBuilder.DropIndex(
                name: "IX_Odevler_HaftalikProgramId",
                table: "Odevler");

            migrationBuilder.DropColumn(
                name: "Gun",
                table: "Odevler");

            migrationBuilder.DropColumn(
                name: "HaftalikProgramId",
                table: "Odevler");
        }
    }
}
