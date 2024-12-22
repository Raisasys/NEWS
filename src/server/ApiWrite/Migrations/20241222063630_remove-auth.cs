using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWrite.Migrations
{
    /// <inheritdoc />
    public partial class removeauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShouldAuthenticated",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ShouldAuthenticated",
                table: "GroupNews");

            migrationBuilder.DropColumn(
                name: "ShouldAuthenticated",
                table: "GroupAnnouncement");

            migrationBuilder.DropColumn(
                name: "ShouldAuthenticated",
                table: "Announcement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShouldAuthenticated",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldAuthenticated",
                table: "GroupNews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldAuthenticated",
                table: "GroupAnnouncement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldAuthenticated",
                table: "Announcement",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
