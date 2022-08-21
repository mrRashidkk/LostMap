using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostMap.DAL.Migrations
{
    public partial class UpdateFindingLoss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Lost",
                table: "Losses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Losses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Found",
                table: "Findings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Findings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2db91594-9c6b-4017-a21a-fc1854340f5a"),
                column: "PasswordHash",
                value: "$2a$11$5uew7fgsf.lf4OsaCiZVn.daLN74wG8CcxVFk03mZsezEJg2ol/N6");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lost",
                table: "Losses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Losses");

            migrationBuilder.DropColumn(
                name: "Found",
                table: "Findings");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Findings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2db91594-9c6b-4017-a21a-fc1854340f5a"),
                column: "PasswordHash",
                value: "$2a$11$5APl2yMVlJI2oQSY43lFcetYN1j.YHvUbQztfMkAVWIccTxN.k7ai");
        }
    }
}
