using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity_App.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MidName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number1 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Number2 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Satus = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedImg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PicPath1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PicPath2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PicPath3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic1 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pic2 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pic3 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedImg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedImg_SavedClients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "SavedClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedImg_ClientId",
                table: "SavedImg",
                column: "ClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedImg");

            migrationBuilder.DropTable(
                name: "SavedClients");
        }
    }
}
