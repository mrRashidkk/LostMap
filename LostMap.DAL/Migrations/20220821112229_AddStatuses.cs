using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostMap.DAL.Migrations
{
    public partial class AddStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2db91594-9c6b-4017-a21a-fc1854340f5a"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Losses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Findings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("87874def-44e8-46d5-af66-8f7a64559150"), "Closed" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("9e1753c2-9850-40e8-b092-82282711e006"), "Waiting for author" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b3fed4be-85e9-4f70-aeb8-2797a153f813"), "Active" });

            migrationBuilder.CreateIndex(
                name: "IX_Losses_StatusId",
                table: "Losses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_StatusId",
                table: "Findings",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Findings_Statuses_StatusId",
                table: "Findings",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Losses_Statuses_StatusId",
                table: "Losses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Findings_Statuses_StatusId",
                table: "Findings");

            migrationBuilder.DropForeignKey(
                name: "FK_Losses_Statuses_StatusId",
                table: "Losses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Losses_StatusId",
                table: "Losses");

            migrationBuilder.DropIndex(
                name: "IX_Findings_StatusId",
                table: "Findings");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Losses");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Findings");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "RoleId" },
                values: new object[] { new Guid("2db91594-9c6b-4017-a21a-fc1854340f5a"), "admin@admin.com", "Admin", "Admin", "$2a$11$TeLTuvrqBw6BrQH/LSFp5ubhNWDbEv.IwssDZDobnqFAT4Xvm9U8e", null, null, null, new Guid("a1806be9-f082-4a65-97d9-25f4e9a63c47") });
        }
    }
}
