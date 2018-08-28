using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class AddUserFavoriteAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "FavoriteAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAccounts",
                table: "FavoriteAccounts");

            migrationBuilder.RenameTable(
                name: "FavoriteAccounts",
                newName: "UserFavoriteAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAccounts_ApplicationUserId",
                table: "UserFavoriteAccounts",
                newName: "IX_UserFavoriteAccounts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteAccounts",
                table: "UserFavoriteAccounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FavoriteAccount",
                columns: table => new
                {
                    accountNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAccount", x => x.accountNumber);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropTable(
                name: "FavoriteAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteAccounts",
                table: "UserFavoriteAccounts");

            migrationBuilder.RenameTable(
                name: "UserFavoriteAccounts",
                newName: "FavoriteAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId",
                table: "FavoriteAccounts",
                newName: "IX_FavoriteAccounts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAccounts",
                table: "FavoriteAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "FavoriteAccounts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
