using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identification.Api.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesUsers",
                columns: table => new
                {
                    RoleUsersId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsers", x => x.RoleUsersId);
                });

            migrationBuilder.CreateTable(
                name: "SavedClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MidName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FIO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PassportSeries = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PassportInn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PassportIssuedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    AparmentNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PassportRegion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PassportCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PassportDistrict = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PassportStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PassportHouseNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PassportAparmentNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MartialStatus = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Number1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Number2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PassportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PassportEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreditSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditTime = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleUsersId = table.Column<short>(type: "smallint", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Logins_RolesUsers_RoleUsersId",
                        column: x => x.RoleUsersId,
                        principalTable: "RolesUsers",
                        principalColumn: "RoleUsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedImg",
                columns: table => new
                {
                    ImgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pic1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Pic2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Pic3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedImg", x => x.ImgId);
                    table.ForeignKey(
                        name: "FK_SavedImg_SavedClients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SavedClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Logins_UserId",
                        column: x => x.UserId,
                        principalTable: "Logins",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_RoleUsersId",
                table: "Logins",
                column: "RoleUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedImg_ClientId",
                table: "SavedImg",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SavedImg");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "SavedClients");

            migrationBuilder.DropTable(
                name: "RolesUsers");
        }
    }
}
