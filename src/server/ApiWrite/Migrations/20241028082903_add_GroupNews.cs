using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWrite.Migrations
{
    /// <inheritdoc />
    public partial class add_GroupNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupNews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDuration = table.Column<int>(type: "int", nullable: false),
                    OwnerScopeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShouldAuthenticated = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_GroupNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupNews_AccessEntityItems",
                columns: table => new
                {
                    GroupNewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNews_AccessEntityItems", x => new { x.GroupNewsId, x.Id });
                    table.ForeignKey(
                        name: "FK_GroupNews_AccessEntityItems_GroupNews_GroupNewsId",
                        column: x => x.GroupNewsId,
                        principalTable: "GroupNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupNewsItem",
                columns: table => new
                {
                    GroupNewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNewsItem", x => new { x.GroupNewsId, x.Id });
                    table.ForeignKey(
                        name: "FK_GroupNewsItem_GroupNews_GroupNewsId",
                        column: x => x.GroupNewsId,
                        principalTable: "GroupNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupNews_IsDeleted",
                table: "GroupNews",
                column: "IsDeleted",
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupNews_AccessEntityItems");

            migrationBuilder.DropTable(
                name: "GroupNewsItem");

            migrationBuilder.DropTable(
                name: "GroupNews");
        }
    }
}
