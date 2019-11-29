using Microsoft.EntityFrameworkCore.Migrations;

namespace UserList.Migrations
{
    public partial class DeleteBehaviourChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedUsers_Users_UserId",
                table: "ConnectedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedUsers_Users_UserId",
                table: "ConnectedUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedUsers_Users_UserId",
                table: "ConnectedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedUsers_Users_UserId",
                table: "ConnectedUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
