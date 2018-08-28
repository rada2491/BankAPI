using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class FavoriteUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accountOwner",
                table: "FavoriteAccounts",
                newName: "favoriteOwner");

            migrationBuilder.AddColumn<string>(
                name: "accountNumber",
                table: "FavoriteAccounts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accountNumber",
                table: "FavoriteAccounts");

            migrationBuilder.RenameColumn(
                name: "favoriteOwner",
                table: "FavoriteAccounts",
                newName: "accountOwner");
        }
    }
}
