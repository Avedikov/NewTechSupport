using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 23, 30, 13, 71, DateTimeKind.Utc).AddTicks(5076), "$2a$11$Akk3m7FdGWAAMATw/xFZB.l.iLogPc2EQ8KGbDm3HnHXE6LRC59Cm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 23, 30, 13, 246, DateTimeKind.Utc).AddTicks(8321), "$2a$11$l65.kr5Ci4oCSTM9hp3i3.RYMDp/pgxFpdkZ1Jl4jlfcJzB0WbK.K" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
