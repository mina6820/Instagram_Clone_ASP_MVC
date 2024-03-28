using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class Dbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_storyPhoto_PhotoId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_storyPhoto_Stories_storyId",
                table: "storyPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_storyPhoto",
                table: "storyPhoto");

            migrationBuilder.RenameTable(
                name: "storyPhoto",
                newName: "SrofilePhoto");

            migrationBuilder.RenameIndex(
                name: "IX_storyPhoto_storyId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "storyPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_SrofilePhoto_storyId",
                table: "storyPhoto",
                newName: "IX_storyPhoto_storyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_storyPhoto",
                table: "storyPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_storyPhoto_PhotoId",
                table: "Stories",
                column: "PhotoId",
                principalTable: "storyPhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_storyPhoto_Stories_storyId",
                table: "storyPhoto",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
