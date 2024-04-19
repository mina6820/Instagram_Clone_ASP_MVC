using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Instagram_Clone.Migrations
{
    public partial class AddFollowRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowRequest_notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowRequest_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowRequest_notifications_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FollowRequest_notifications_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowRequest_notifications_ReceiverId",
                table: "FollowRequest_notifications",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowRequest_notifications_SenderId",
                table: "FollowRequest_notifications",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowRequest_notifications");
        }
    }
}
