using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWrite.Migrations
{
    /// <inheritdoc />
    public partial class GroupAnnouncement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupAnnouncementId",
                table: "CommunicationItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupAnnouncement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerScopeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShouldAuthenticated = table.Column<bool>(type: "bit", nullable: false),
                    Archived_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Archived_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Published_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAnnouncement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupAnnouncement_AccessEntityItems",
                columns: table => new
                {
                    GroupAnnouncementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAnnouncement_AccessEntityItems", x => new { x.GroupAnnouncementId, x.Id });
                    table.ForeignKey(
                        name: "FK_GroupAnnouncement_AccessEntityItems_GroupAnnouncement_GroupAnnouncementId",
                        column: x => x.GroupAnnouncementId,
                        principalTable: "GroupAnnouncement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupAnnouncementItem",
                columns: table => new
                {
                    GroupAnnouncementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnouncementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAnnouncementItem", x => new { x.GroupAnnouncementId, x.Id });
                    table.ForeignKey(
                        name: "FK_GroupAnnouncementItem_GroupAnnouncement_GroupAnnouncementId",
                        column: x => x.GroupAnnouncementId,
                        principalTable: "GroupAnnouncement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationItem_GroupAnnouncementId",
                table: "CommunicationItem",
                column: "GroupAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAnnouncement_IsDeleted",
                table: "GroupAnnouncement",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationItem_GroupAnnouncement_GroupAnnouncementId",
                table: "CommunicationItem",
                column: "GroupAnnouncementId",
                principalTable: "GroupAnnouncement",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationItem_GroupAnnouncement_GroupAnnouncementId",
                table: "CommunicationItem");

            migrationBuilder.DropTable(
                name: "GroupAnnouncement_AccessEntityItems");

            migrationBuilder.DropTable(
                name: "GroupAnnouncementItem");

            migrationBuilder.DropTable(
                name: "GroupAnnouncement");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationItem_GroupAnnouncementId",
                table: "CommunicationItem");

            migrationBuilder.DropColumn(
                name: "GroupAnnouncementId",
                table: "CommunicationItem");
        }
    }
}
