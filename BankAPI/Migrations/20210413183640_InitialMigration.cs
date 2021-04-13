using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(maxLength: 256, nullable: true),
                    County = table.Column<string>(maxLength: 256, nullable: true),
                    City = table.Column<string>(maxLength: 256, nullable: true),
                    Street = table.Column<string>(maxLength: 256, nullable: true),
                    Number = table.Column<string>(maxLength: 256, nullable: true),
                    Block = table.Column<string>(maxLength: 256, nullable: true),
                    Stairway = table.Column<string>(maxLength: 256, nullable: true),
                    Apartment = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordToken = table.Column<string>(maxLength: 256, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    CNP = table.Column<string>(maxLength: 256, nullable: false),
                    CI = table.Column<string>(maxLength: 256, nullable: false),
                    ConfirmationKey = table.Column<string>(maxLength: 256, nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    ActionsHistory = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordToken = table.Column<string>(maxLength: 256, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    CNP = table.Column<string>(maxLength: 256, nullable: false),
                    CI = table.Column<string>(maxLength: 256, nullable: false),
                    ConfirmationKey = table.Column<string>(maxLength: 256, nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 128, nullable: true),
                    HomeAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Address_HomeAddressId",
                        column: x => x.HomeAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    IBAN = table.Column<string>(maxLength: 256, nullable: false),
                    Type = table.Column<string>(maxLength: 256, nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    Currency = table.Column<string>(maxLength: 64, nullable: true),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.IBAN);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(maxLength: 256, nullable: false),
                    HolderIBAN = table.Column<string>(maxLength: 256, nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CVV = table.Column<string>(maxLength: 32, nullable: true),
                    HolderFullName = table.Column<string>(maxLength: 256, nullable: true),
                    BankAccountIBAN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_Cards_BankAccounts_BankAccountIBAN",
                        column: x => x.BankAccountIBAN,
                        principalTable: "BankAccounts",
                        principalColumn: "IBAN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(maxLength: 256, nullable: false),
                    SenderIBAN = table.Column<string>(maxLength: 256, nullable: false),
                    ReceiverIBAN = table.Column<string>(maxLength: 256, nullable: false),
                    ReceiverFullName = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Currency = table.Column<string>(maxLength: 64, nullable: false),
                    BankAccountIBAN1 = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstPaymentDate = table.Column<DateTime>(nullable: true),
                    LastPaymentDate = table.Column<DateTime>(nullable: true),
                    DaysInterval = table.Column<int>(nullable: true),
                    IsMonthly = table.Column<bool>(nullable: true),
                    BankAccountIBAN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankAccountIBAN",
                        column: x => x.BankAccountIBAN,
                        principalTable: "BankAccounts",
                        principalColumn: "IBAN",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankAccountIBAN1",
                        column: x => x.BankAccountIBAN1,
                        principalTable: "BankAccounts",
                        principalColumn: "IBAN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_CustomerId",
                table: "BankAccounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BankAccountIBAN",
                table: "Cards",
                column: "BankAccountIBAN");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_HomeAddressId",
                table: "Customers",
                column: "HomeAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountIBAN",
                table: "Transactions",
                column: "BankAccountIBAN");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountIBAN1",
                table: "Transactions",
                column: "BankAccountIBAN1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
