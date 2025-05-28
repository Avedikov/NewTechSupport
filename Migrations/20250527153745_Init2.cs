using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 15, 37, 45, 143, DateTimeKind.Utc).AddTicks(8145), "$2a$11$DdypK4aXn4IcidwxvjQKPOxKwRP5Tn95S0o.Oj8kS5yzQbx4maAF." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 15, 37, 45, 298, DateTimeKind.Utc).AddTicks(57), "$2a$11$m7hRMlCXhYF.sLODRW4VIewuts75/9A9ycCvkDbWIVlT8ZpaoI/W." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 5, 27, 15, 25, 17, 736, DateTimeKind.Utc).AddTicks(6201), "12345", "$2a$11$PeS4ElMWe8DCsd5Dhlmu5u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 5, 27, 15, 25, 17, 737, DateTimeKind.Utc).AddTicks(8888), "123456", "$2a$11$CO2ODmpW8tN1JWMlwUF1te" });
        }
    }
}
