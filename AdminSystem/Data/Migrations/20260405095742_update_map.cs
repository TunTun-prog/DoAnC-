using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_map : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "QuayHangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "QuayHangs",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "QuayHangs",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "QuayHangs");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "QuayHangs");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "QuayHangs");
        }
    }
}
