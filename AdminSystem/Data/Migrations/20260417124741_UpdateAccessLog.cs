using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccessLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kiosk",
                table: "AccessLogs");

            migrationBuilder.AddColumn<int>(
                name: "QuayHangId",
                table: "AccessLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessLogs_QuayHangId",
                table: "AccessLogs",
                column: "QuayHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessLogs_QuayHangs_QuayHangId",
                table: "AccessLogs",
                column: "QuayHangId",
                principalTable: "QuayHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessLogs_QuayHangs_QuayHangId",
                table: "AccessLogs");

            migrationBuilder.DropIndex(
                name: "IX_AccessLogs_QuayHangId",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "QuayHangId",
                table: "AccessLogs");

            migrationBuilder.AddColumn<string>(
                name: "Kiosk",
                table: "AccessLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
