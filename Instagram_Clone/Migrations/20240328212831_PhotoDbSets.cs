using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Instagram_Clone.Migrations
{
    /// <inheritdoc />
    public partial class PhotoDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_FolloweeId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_FollowerId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Photos_photoId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Messages_MessageId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Stories_storyId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Photos_PhotoId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MessageId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PostId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Connections",
                newName: "UserRelationship");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "storyPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_FollowerId",
                table: "UserRelationship",
                newName: "IX_UserRelationship_FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_FolloweeId",
                table: "UserRelationship",
                newName: "IX_UserRelationship_FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_storyId",
                table: "storyPhoto",
                newName: "IX_storyPhoto_storyId");

            migrationBuilder.AlterColumn<int>(
                name: "storyId",
                table: "storyPhoto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRelationship",
                table: "UserRelationship",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_storyPhoto",
                table: "storyPhoto",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MessagePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessagePhoto_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PostPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPhoto_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePhoto_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessagePhoto_MessageId",
                table: "MessagePhoto",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPhoto_PostId",
                table: "PostPhoto",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhoto_UserId",
                table: "ProfilePhoto",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfilePhoto_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "ProfilePhoto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessagePhoto_photoId",
                table: "Messages",
                column: "photoId",
                principalTable: "MessagePhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelationship_AspNetUsers_FolloweeId",
                table: "UserRelationship",
                column: "FolloweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelationship_AspNetUsers_FollowerId",
                table: "UserRelationship",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProfilePhoto_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessagePhoto_photoId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_storyPhoto_PhotoId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_storyPhoto_Stories_storyId",
                table: "storyPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRelationship_AspNetUsers_FolloweeId",
                table: "UserRelationship");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRelationship_AspNetUsers_FollowerId",
                table: "UserRelationship");

            migrationBuilder.DropTable(
                name: "MessagePhoto");

            migrationBuilder.DropTable(
                name: "PostPhoto");

            migrationBuilder.DropTable(
                name: "ProfilePhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRelationship",
                table: "UserRelationship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_storyPhoto",
                table: "storyPhoto");

            migrationBuilder.RenameTable(
                name: "UserRelationship",
                newName: "Connections");

            migrationBuilder.RenameTable(
                name: "storyPhoto",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_UserRelationship_FollowerId",
                table: "Connections",
                newName: "IX_Connections_FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRelationship_FolloweeId",
                table: "Connections",
                newName: "IX_Connections_FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_storyPhoto_storyId",
                table: "Photos",
                newName: "IX_Photos_storyId");

            migrationBuilder.AlterColumn<int>(
                name: "storyId",
                table: "Photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Photos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MessageId",
                table: "Photos",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostId",
                table: "Photos",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_FolloweeId",
                table: "Connections",
                column: "FolloweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_FollowerId",
                table: "Connections",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Photos_photoId",
                table: "Messages",
                column: "photoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_UserId",
                table: "Photos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Messages_MessageId",
                table: "Photos",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Stories_storyId",
                table: "Photos",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Photos_PhotoId",
                table: "Stories",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
