using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 3, 21, 48, 5, 593, DateTimeKind.Utc).AddTicks(6922), "$2a$11$RBmv4vMewCglKgz37/J2funVWjW8UXiKcsQHcwKrYFpLw.P1aeVvS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 3, 21, 48, 5, 757, DateTimeKind.Utc).AddTicks(2133), "$2a$11$VCImooenYPMKiGQRoqJ/mOgegGNVDYF.0/qsH7h6VE0qKAsDx4gnG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
