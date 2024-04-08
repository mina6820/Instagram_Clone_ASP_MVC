using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class StoryEdit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StoryPhoto_PhotoId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_StoryPhoto_storyId",
                table: "StoryPhoto");

            migrationBuilder.DropIndex(
                name: "IX_Stories_PhotoId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "LifeTime",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Stories");

            migrationBuilder.AddColumn<long>(
                name: "LifeTimeTicks",
                table: "Stories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StoryPhoto_storyId",
                table: "StoryPhoto",
                column: "storyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoryPhoto_storyId",
                table: "StoryPhoto");

            migrationBuilder.DropColumn(
                name: "LifeTimeTicks",
                table: "Stories");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LifeTime",
                table: "Stories",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoryPhoto_storyId",
                table: "StoryPhoto",
                column: "storyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_PhotoId",
                table: "Stories",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StoryPhoto_PhotoId",
                table: "Stories",
                column: "PhotoId",
                principalTable: "StoryPhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
