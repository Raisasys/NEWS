using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWrite.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewsId",
                table: "CommunicationItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationItem_NewsId",
                table: "CommunicationItem",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationItem_News_NewsId",
                table: "CommunicationItem",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationItem_News_NewsId",
                table: "CommunicationItem");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationItem_NewsId",
                table: "CommunicationItem");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "CommunicationItem");
        }
    }
}
