using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class StoryEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryView");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Stories",
                newName: "CreatedAt");

            migrationBuilder.CreateTable(
                name: "StoryViewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryViewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryViewers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StoryViewers_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryViewers_StoryId",
                table: "StoryViewers",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryViewers_UserId",
                table: "StoryViewers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryViewers");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Stories",
                newName: "Date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Stories",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "StoryView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryView_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StoryView_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryView_StoryId",
                table: "StoryView",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryView_UserId",
                table: "StoryView",
                column: "UserId");
        }
    }
}
