using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YksKoclukSistemi.Migrations
{
    /// <inheritdoc />
    public partial class ipek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DenemeSonuclari_Kullanicilar_OgrenciId",
                table: "DenemeSonuclari");

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Kullanicilar_KocId",
                table: "Kullanicilar");

            migrationBuilder.DropForeignKey(
                name: "FK_Programlar_Kullanicilar_OgrenciId",
                table: "Programlar");

            migrationBuilder.DropTable(
                name: "Odevler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programlar",
                table: "Programlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DenemeSonuclari",
                table: "DenemeSonuclari");

            migrationBuilder.RenameTable(
                name: "Programlar",
                newName: "HaftalikProgramlar");

            migrationBuilder.RenameTable(
                name: "DenemeSonuclari",
                newName: "DenemeSonuc");

            migrationBuilder.RenameIndex(
                name: "IX_Programlar_OgrenciId",
                table: "HaftalikProgramlar",
                newName: "IX_HaftalikProgramlar_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_DenemeSonuclari_OgrenciId",
                table: "DenemeSonuc",
                newName: "IX_DenemeSonuc_OgrenciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HaftalikProgramlar",
                table: "HaftalikProgramlar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DenemeSonuc",
                table: "DenemeSonuc",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Gorevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YapildiMi = table.Column<bool>(type: "bit", nullable: false),
                    HaftalikProgramId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gorevler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gorevler_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gorevler_HaftalikProgramlar_HaftalikProgramId",
                        column: x => x.HaftalikProgramId,
                        principalTable: "HaftalikProgramlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dersler",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "TYT Matematik" },
                    { 2, "AYT Matematik" },
                    { 3, "Geometri" },
                    { 4, "Fizik" },
                    { 5, "Kimya" },
                    { 6, "Biyoloji" },
                    { 7, "Türkçe" },
                    { 8, "Tarih" },
                    { 9, "Coğrafya" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_DersId",
                table: "Gorevler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_HaftalikProgramId",
                table: "Gorevler",
                column: "HaftalikProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_DenemeSonuc_Kullanicilar_OgrenciId",
                table: "DenemeSonuc",
                column: "OgrenciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HaftalikProgramlar_Kullanicilar_OgrenciId",
                table: "HaftalikProgramlar",
                column: "OgrenciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Kullanicilar_KocId",
                table: "Kullanicilar",
                column: "KocId",
                principalTable: "Kullanicilar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DenemeSonuc_Kullanicilar_OgrenciId",
                table: "DenemeSonuc");

            migrationBuilder.DropForeignKey(
                name: "FK_HaftalikProgramlar_Kullanicilar_OgrenciId",
                table: "HaftalikProgramlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Kullanicilar_KocId",
                table: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Gorevler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HaftalikProgramlar",
                table: "HaftalikProgramlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DenemeSonuc",
                table: "DenemeSonuc");

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Dersler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.RenameTable(
                name: "HaftalikProgramlar",
                newName: "Programlar");

            migrationBuilder.RenameTable(
                name: "DenemeSonuc",
                newName: "DenemeSonuclari");

            migrationBuilder.RenameIndex(
                name: "IX_HaftalikProgramlar_OgrenciId",
                table: "Programlar",
                newName: "IX_Programlar_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_DenemeSonuc_OgrenciId",
                table: "DenemeSonuclari",
                newName: "IX_DenemeSonuclari_OgrenciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programlar",
                table: "Programlar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DenemeSonuclari",
                table: "DenemeSonuclari",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Odevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersId = table.Column<int>(type: "int", nullable: false),
                    HaftalikProgramId = table.Column<int>(type: "int", nullable: true),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SonTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YapildiMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odevler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odevler_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odevler_Kullanicilar_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odevler_Programlar_HaftalikProgramId",
                        column: x => x.HaftalikProgramId,
                        principalTable: "Programlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odevler_DersId",
                table: "Odevler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Odevler_HaftalikProgramId",
                table: "Odevler",
                column: "HaftalikProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Odevler_OgrenciId",
                table: "Odevler",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_DenemeSonuclari_Kullanicilar_OgrenciId",
                table: "DenemeSonuclari",
                column: "OgrenciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Kullanicilar_KocId",
                table: "Kullanicilar",
                column: "KocId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programlar_Kullanicilar_OgrenciId",
                table: "Programlar",
                column: "OgrenciId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
