using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWrite.Migrations
{
    /// <inheritdoc />
    public partial class _05293 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementFiles_Announcement_AnnouncementId",
                table: "AnnouncementFiles");

            migrationBuilder.AlterColumn<long>(
                name: "AnnouncementId",
                table: "AnnouncementFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementFiles_Announcement_AnnouncementId",
                table: "AnnouncementFiles",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementFiles_Announcement_AnnouncementId",
                table: "AnnouncementFiles");

            migrationBuilder.AlterColumn<long>(
                name: "AnnouncementId",
                table: "AnnouncementFiles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementFiles_Announcement_AnnouncementId",
                table: "AnnouncementFiles",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id");
        }
    }
}
