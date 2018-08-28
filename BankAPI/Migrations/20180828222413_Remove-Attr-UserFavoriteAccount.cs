using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class RemoveAttrUserFavoriteAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteAccounts",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropColumn(
                name: "accountNumber",
                table: "UserFavoriteAccounts");

            migrationBuilder.RenameColumn(
                name: "favoriteOwner",
                table: "UserFavoriteAccounts",
                newName: "FavoriteAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserFavoriteAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FavoriteAccountId",
                table: "UserFavoriteAccounts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "UserFavoriteAccounts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteAccounts",
                table: "UserFavoriteAccounts",
                columns: new[] { "ApplicationUserId", "FavoriteAccountId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId1",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteAccounts_FavoriteAccountId",
                table: "UserFavoriteAccounts",
                column: "FavoriteAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId1",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAccounts_FavoriteAccount_FavoriteAccountId",
                table: "UserFavoriteAccounts",
                column: "FavoriteAccountId",
                principalTable: "FavoriteAccount",
                principalColumn: "accountNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId1",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAccounts_FavoriteAccount_FavoriteAccountId",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteAccounts",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId1",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserFavoriteAccounts_FavoriteAccountId",
                table: "UserFavoriteAccounts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "UserFavoriteAccounts");

            migrationBuilder.RenameColumn(
                name: "FavoriteAccountId",
                table: "UserFavoriteAccounts",
                newName: "favoriteOwner");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserFavoriteAccounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "favoriteOwner",
                table: "UserFavoriteAccounts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserFavoriteAccounts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "accountNumber",
                table: "UserFavoriteAccounts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteAccounts",
                table: "UserFavoriteAccounts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteAccounts_ApplicationUserId",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAccounts_AspNetUsers_ApplicationUserId",
                table: "UserFavoriteAccounts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
