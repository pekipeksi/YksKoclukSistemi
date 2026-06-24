using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YksKoclukSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AlanEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alan",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alan",
                table: "Kullanicilar");
        }
    }
}
