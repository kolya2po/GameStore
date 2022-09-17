using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.DAL.Migrations
{
    public partial class AddLinkFromGameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_AuthorId",
                table: "Games",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_AuthorId",
                table: "Games",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_AuthorId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_AuthorId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Games");
        }
    }
}
