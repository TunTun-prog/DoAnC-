using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_audio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioPath",
                table: "QuayHangs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioPath",
                table: "QuayHangs");
        }
    }
}
