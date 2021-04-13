using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class signupConfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationKey",
                table: "Customers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationKey",
                table: "Admins",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Admins",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationKey",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ConfirmationKey",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Admins");
        }
    }
}
