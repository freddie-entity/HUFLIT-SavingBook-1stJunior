using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace test.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    IdBank = table.Column<string>(nullable: false),
                    NameBank = table.Column<string>(maxLength: 150, nullable: false),
                    AddressBank = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.IdBank);
                });

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    IdBookType = table.Column<string>(nullable: false),
                    NameBookType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.IdBookType);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdCust = table.Column<string>(nullable: false),
                    NameCust = table.Column<string>(nullable: true),
                    DOBCust = table.Column<DateTime>(nullable: false),
                    AddressCust = table.Column<string>(nullable: true),
                    PhoneCust = table.Column<string>(nullable: true),
                    IDCardCust = table.Column<string>(nullable: true),
                    IDCardGrantedDayCust = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdCust);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    IdInterest = table.Column<string>(nullable: false),
                    AppliedFrom = table.Column<DateTime>(nullable: false),
                    AppliedTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.IdInterest);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    IdReport = table.Column<string>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.IdReport);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    IdTerm = table.Column<string>(nullable: false),
                    NameTerm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.IdTerm);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    IdS = table.Column<string>(nullable: false),
                    NameS = table.Column<string>(nullable: true),
                    IdBank = table.Column<string>(nullable: true),
                    DOBS = table.Column<DateTime>(nullable: false),
                    IDCardS = table.Column<string>(nullable: true),
                    IDCardGrantedDayS = table.Column<DateTime>(nullable: false),
                    PositionS = table.Column<string>(nullable: true),
                    PhoneS = table.Column<string>(nullable: true),
                    WorkingStatus = table.Column<bool>(nullable: false),
                    StartWorking = table.Column<DateTime>(nullable: false),
                    EndWorking = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.IdS);
                    table.ForeignKey(
                        name: "FK_Staffs_Banks_IdBank",
                        column: x => x.IdBank,
                        principalTable: "Banks",
                        principalColumn: "IdBank",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailReports",
                columns: table => new
                {
                    IdDR = table.Column<string>(nullable: false),
                    IdReport = table.Column<string>(nullable: true),
                    IdBookType = table.Column<string>(nullable: true),
                    OpenedBooks = table.Column<int>(nullable: false),
                    ClosedBooks = table.Column<int>(nullable: false),
                    TotalBooks = table.Column<int>(nullable: false),
                    TotalRevenue = table.Column<double>(nullable: false),
                    TotalExpense = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailReports", x => x.IdDR);
                    table.ForeignKey(
                        name: "FK_DetailReports_BookTypes_IdBookType",
                        column: x => x.IdBookType,
                        principalTable: "BookTypes",
                        principalColumn: "IdBookType",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailReports_Reports_IdReport",
                        column: x => x.IdReport,
                        principalTable: "Reports",
                        principalColumn: "IdReport",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailInterests",
                columns: table => new
                {
                    IdDI = table.Column<string>(nullable: false),
                    IdInterest = table.Column<string>(nullable: true),
                    IdTerm = table.Column<string>(nullable: true),
                    InterestRateDI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailInterests", x => x.IdDI);
                    table.ForeignKey(
                        name: "FK_DetailInterests_Interests_IdInterest",
                        column: x => x.IdInterest,
                        principalTable: "Interests",
                        principalColumn: "IdInterest",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailInterests_Terms_IdTerm",
                        column: x => x.IdTerm,
                        principalTable: "Terms",
                        principalColumn: "IdTerm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavingBooks",
                columns: table => new
                {
                    IdSB = table.Column<string>(nullable: false),
                    IdCust = table.Column<string>(nullable: true),
                    IdS = table.Column<string>(nullable: true),
                    IdBookType = table.Column<string>(nullable: true),
                    IdTerm = table.Column<string>(nullable: true),
                    DepositsSB = table.Column<double>(nullable: false),
                    OpenDaySB = table.Column<DateTime>(nullable: false),
                    DueDaySB = table.Column<DateTime>(nullable: false),
                    InterestPaymentMethodSB = table.Column<bool>(nullable: false),
                    InterestReceivingAccount = table.Column<string>(nullable: true),
                    CurrentBalance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingBooks", x => x.IdSB);
                    table.ForeignKey(
                        name: "FK_SavingBooks_BookTypes_IdBookType",
                        column: x => x.IdBookType,
                        principalTable: "BookTypes",
                        principalColumn: "IdBookType",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingBooks_Customers_IdCust",
                        column: x => x.IdCust,
                        principalTable: "Customers",
                        principalColumn: "IdCust",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingBooks_Staffs_IdS",
                        column: x => x.IdS,
                        principalTable: "Staffs",
                        principalColumn: "IdS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavingBooks_Terms_IdTerm",
                        column: x => x.IdTerm,
                        principalTable: "Terms",
                        principalColumn: "IdTerm",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IdS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Staffs_IdS",
                        column: x => x.IdS,
                        principalTable: "Staffs",
                        principalColumn: "IdS",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositPapers",
                columns: table => new
                {
                    IdDP = table.Column<string>(nullable: false),
                    IdSB = table.Column<string>(nullable: true),
                    IdCust = table.Column<string>(nullable: true),
                    IdS = table.Column<string>(nullable: true),
                    IdBank = table.Column<string>(nullable: true),
                    IdBookType = table.Column<string>(nullable: true),
                    DepositsDP = table.Column<double>(nullable: false),
                    TransactionTimeDP = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositPapers", x => x.IdDP);
                    table.ForeignKey(
                        name: "FK_DepositPapers_Banks_IdBank",
                        column: x => x.IdBank,
                        principalTable: "Banks",
                        principalColumn: "IdBank",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositPapers_BookTypes_IdBookType",
                        column: x => x.IdBookType,
                        principalTable: "BookTypes",
                        principalColumn: "IdBookType",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositPapers_Customers_IdCust",
                        column: x => x.IdCust,
                        principalTable: "Customers",
                        principalColumn: "IdCust",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositPapers_Staffs_IdS",
                        column: x => x.IdS,
                        principalTable: "Staffs",
                        principalColumn: "IdS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepositPapers_SavingBooks_IdSB",
                        column: x => x.IdSB,
                        principalTable: "SavingBooks",
                        principalColumn: "IdSB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalPapers",
                columns: table => new
                {
                    IdWP = table.Column<string>(nullable: false),
                    IdSB = table.Column<string>(nullable: true),
                    IdCust = table.Column<string>(nullable: true),
                    IdS = table.Column<string>(nullable: true),
                    IdBank = table.Column<string>(nullable: true),
                    WithdrawalsWP = table.Column<double>(nullable: false),
                    TransactionTimeWP = table.Column<DateTime>(nullable: false),
                    BookTypeIdBookType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalPapers", x => x.IdWP);
                    table.ForeignKey(
                        name: "FK_WithdrawalPapers_BookTypes_BookTypeIdBookType",
                        column: x => x.BookTypeIdBookType,
                        principalTable: "BookTypes",
                        principalColumn: "IdBookType",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalPapers_Banks_IdBank",
                        column: x => x.IdBank,
                        principalTable: "Banks",
                        principalColumn: "IdBank",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalPapers_Customers_IdCust",
                        column: x => x.IdCust,
                        principalTable: "Customers",
                        principalColumn: "IdCust",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalPapers_Staffs_IdS",
                        column: x => x.IdS,
                        principalTable: "Staffs",
                        principalColumn: "IdS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawalPapers_SavingBooks_IdSB",
                        column: x => x.IdSB,
                        principalTable: "SavingBooks",
                        principalColumn: "IdSB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepositPapers_IdBank",
                table: "DepositPapers",
                column: "IdBank");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPapers_IdBookType",
                table: "DepositPapers",
                column: "IdBookType");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPapers_IdCust",
                table: "DepositPapers",
                column: "IdCust");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPapers_IdS",
                table: "DepositPapers",
                column: "IdS");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPapers_IdSB",
                table: "DepositPapers",
                column: "IdSB");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInterests_IdInterest",
                table: "DetailInterests",
                column: "IdInterest");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInterests_IdTerm",
                table: "DetailInterests",
                column: "IdTerm");

            migrationBuilder.CreateIndex(
                name: "IX_DetailReports_IdBookType",
                table: "DetailReports",
                column: "IdBookType");

            migrationBuilder.CreateIndex(
                name: "IX_DetailReports_IdReport",
                table: "DetailReports",
                column: "IdReport");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SavingBooks_IdBookType",
                table: "SavingBooks",
                column: "IdBookType");

            migrationBuilder.CreateIndex(
                name: "IX_SavingBooks_IdCust",
                table: "SavingBooks",
                column: "IdCust");

            migrationBuilder.CreateIndex(
                name: "IX_SavingBooks_IdS",
                table: "SavingBooks",
                column: "IdS");

            migrationBuilder.CreateIndex(
                name: "IX_SavingBooks_IdTerm",
                table: "SavingBooks",
                column: "IdTerm");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdBank",
                table: "Staffs",
                column: "IdBank");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdS",
                table: "Users",
                column: "IdS");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalPapers_BookTypeIdBookType",
                table: "WithdrawalPapers",
                column: "BookTypeIdBookType");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalPapers_IdBank",
                table: "WithdrawalPapers",
                column: "IdBank");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalPapers_IdCust",
                table: "WithdrawalPapers",
                column: "IdCust");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalPapers_IdS",
                table: "WithdrawalPapers",
                column: "IdS");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalPapers_IdSB",
                table: "WithdrawalPapers",
                column: "IdSB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositPapers");

            migrationBuilder.DropTable(
                name: "DetailInterests");

            migrationBuilder.DropTable(
                name: "DetailReports");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "WithdrawalPapers");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SavingBooks");

            migrationBuilder.DropTable(
                name: "BookTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
