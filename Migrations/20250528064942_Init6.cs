using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class Init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 6, 49, 42, 32, DateTimeKind.Utc).AddTicks(4209), "$2a$11$Qx4NSahIjky9KDR/FQz/AOjpvN23RSFrw9Nj3jodE0YvirGIw8bey" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 6, 49, 42, 214, DateTimeKind.Utc).AddTicks(94), "$2a$11$NDrhGyHH4jFkMF.z5NBiFOdgW6uxau8lvisfEcIYF2qX85tjnLYqC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
