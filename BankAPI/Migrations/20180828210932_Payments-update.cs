using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class Paymentsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
