using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostMap.DAL.Migrations
{
    public partial class FixTypoLogtitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Losses");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Findings");

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Losses",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Losses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Findings",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Findings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2db91594-9c6b-4017-a21a-fc1854340f5a"),
                column: "PasswordHash",
                value: "$2a$11$TeLTuvrqBw6BrQH/LSFp5ubhNWDbEv.IwssDZDobnqFAT4Xvm9U8e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Losses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Findings");

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Losses",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "Longtitude",
                table: "Losses",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Findings",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "Longtitude",
                table: "Findings",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2db91594-9c6b-4017-a21a-fc1854340f5a"),
                column: "PasswordHash",
                value: "$2a$11$5uew7fgsf.lf4OsaCiZVn.daLN74wG8CcxVFk03mZsezEJg2ol/N6");
        }
    }
}
