using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class updateUserModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 22, 55, 36, 844, DateTimeKind.Utc).AddTicks(726), "$2a$11$WV1D45FhlE/ShxURhvdOZuTgG1dUegEZOXI2wWBPLqL8La4eaRDHq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 22, 55, 37, 26, DateTimeKind.Utc).AddTicks(9122), "$2a$11$5k0fmv3ETvUtCvdWEi6PfOx96iZVINHQ9CYPrIXZkvB.o5kZkCg76" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 22, 47, 57, 337, DateTimeKind.Utc).AddTicks(7745), "$2a$11$0bj/YUi90cMyGEg5cKWl5OGW4JFllQbxwkCATygnCYRer4xp8oCyW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 22, 47, 57, 516, DateTimeKind.Utc).AddTicks(6540), "$2a$11$EsarVoqMsURSooK9MmuPSe1fTnOgbnIhCW/jIMf2kJyEcgZFQctWy" });
        }
    }
}
