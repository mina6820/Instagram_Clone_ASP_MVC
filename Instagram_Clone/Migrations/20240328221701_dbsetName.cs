using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class dbsetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SrofilePhoto_Stories_storyId",
                table: "SrofilePhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_SrofilePhoto_PhotoId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SrofilePhoto",
                table: "SrofilePhoto");

            migrationBuilder.RenameTable(
                name: "SrofilePhoto",
                newName: "StoryPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_SrofilePhoto_storyId",
                table: "StoryPhoto",
                newName: "IX_StoryPhoto_storyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryPhoto",
                table: "StoryPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StoryPhoto_PhotoId",
                table: "Stories",
                column: "PhotoId",
                principalTable: "StoryPhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryPhoto_Stories_storyId",
                table: "StoryPhoto",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StoryPhoto_PhotoId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryPhoto_Stories_storyId",
                table: "StoryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryPhoto",
                table: "StoryPhoto");

            migrationBuilder.RenameTable(
                name: "StoryPhoto",
                newName: "SrofilePhoto");

            migrationBuilder.RenameIndex(
                name: "IX_StoryPhoto_storyId",
                table: "SrofilePhoto",
                newName: "IX_SrofilePhoto_storyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SrofilePhoto",
                table: "SrofilePhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SrofilePhoto_Stories_storyId",
                table: "SrofilePhoto",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_SrofilePhoto_PhotoId",
                table: "Stories",
                column: "PhotoId",
                principalTable: "SrofilePhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
