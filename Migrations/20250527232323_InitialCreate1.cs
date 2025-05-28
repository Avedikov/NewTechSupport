using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 23, 23, 22, 761, DateTimeKind.Utc).AddTicks(154), "$2a$11$tWweW3fYQHxiYiN61qzy1elYbTcdT8GfqHU16iiwV6mBy.mCqJwDy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 23, 23, 22, 942, DateTimeKind.Utc).AddTicks(2852), "$2a$11$pWt9MOP5rH2E5P89dNvX2eBDq2AkT5l4XzVeDwHmpTdhVBq7J4JA." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 55, 34, 195, DateTimeKind.Utc).AddTicks(2255), "$2a$11$g24g8p7iIWu9GhBUqKUkIOeX.NrLai9QD/mFJW0f8uSy9KgRg/TMO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 55, 34, 400, DateTimeKind.Utc).AddTicks(9760), "$2a$11$dThFU4QWWk/tQ4HjKzajluBk9YrdaAacqHwmmaCnkadqaWeiLcKmG" });
        }
    }
}
