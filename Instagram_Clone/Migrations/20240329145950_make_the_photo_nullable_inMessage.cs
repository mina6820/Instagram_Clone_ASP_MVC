using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class make_the_photo_nullable_inMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessagePhoto_photoId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "photoId",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessagePhoto_photoId",
                table: "Messages",
                column: "photoId",
                principalTable: "MessagePhoto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessagePhoto_photoId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "photoId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessagePhoto_photoId",
                table: "Messages",
                column: "photoId",
                principalTable: "MessagePhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
