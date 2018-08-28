using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class ApplicationUserupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId1",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId1",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "UserFavoriteAccounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "UserFavoriteAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId1",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId1",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
