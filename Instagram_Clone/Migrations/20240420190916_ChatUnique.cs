using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class ChatUnique : Migration
    {
        /// <inheritdoc />        

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
        name: "CombinedId",
            table: "Chats",
            nullable: true,
            computedColumnSql: "CASE WHEN SenderId < RecieverId THEN CONCAT(SenderId, '-', RecieverId) ELSE CONCAT(RecieverId, '-', SenderId) END");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_CombinedId",
                table: "Chats",
                column: "CombinedId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chats_CombinedId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "CombinedId",
                table: "Chats");
        }

    }
}
