using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Block = table.Column<string>(nullable: true),
                    Stairway = table.Column<string>(nullable: true),
                    Apartment = table.Column<string>(nullable: true)
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
                    Username = table.Column<string>(nullable: true),
                    PasswordToken = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CNP = table.Column<string>(nullable: true),
                    CI = table.Column<string>(nullable: true),
                    ActionsHistory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NormalDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalDate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    PasswordToken = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CNP = table.Column<string>(nullable: true),
                    CI = table.Column<string>(nullable: true),
                    DateOfBirthId = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    HomeAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_NormalDate_DateOfBirthId",
                        column: x => x.DateOfBirthId,
                        principalTable: "NormalDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IBAN = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
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
                    CardNumber = table.Column<string>(nullable: false),
                    HolderIBAN = table.Column<string>(nullable: true),
                    expirationTimeId = table.Column<int>(nullable: true),
                    CVV = table.Column<string>(nullable: true),
                    HolderFullName = table.Column<string>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Cards_NormalDate_expirationTimeId",
                        column: x => x.expirationTimeId,
                        principalTable: "NormalDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(nullable: false),
                    SenderIBAN = table.Column<string>(nullable: true),
                    ReceiverIBAN = table.Column<string>(nullable: true),
                    ReceiverFullName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    BankAccountIBAN1 = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstPaymentDateId = table.Column<int>(nullable: true),
                    LastPaymentDateId = table.Column<int>(nullable: true),
                    DaysInterval = table.Column<int>(nullable: true),
                    isMonthly = table.Column<bool>(nullable: true),
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
                        name: "FK_Transactions_NormalDate_FirstPaymentDateId",
                        column: x => x.FirstPaymentDateId,
                        principalTable: "NormalDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_NormalDate_LastPaymentDateId",
                        column: x => x.LastPaymentDateId,
                        principalTable: "NormalDate",
                        principalColumn: "Id",
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
                name: "IX_Cards_expirationTimeId",
                table: "Cards",
                column: "expirationTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DateOfBirthId",
                table: "Customers",
                column: "DateOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_HomeAddressId",
                table: "Customers",
                column: "HomeAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountIBAN",
                table: "Transactions",
                column: "BankAccountIBAN");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FirstPaymentDateId",
                table: "Transactions",
                column: "FirstPaymentDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LastPaymentDateId",
                table: "Transactions",
                column: "LastPaymentDateId");

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
                name: "NormalDate");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
