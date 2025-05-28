using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 48, 21, 405, DateTimeKind.Utc).AddTicks(3518), "$2a$11$c4kIyQIstz8G8Xco0QS0heqJT7LpVpMiiemtnuGRc2TMTuuatq1P." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 48, 21, 585, DateTimeKind.Utc).AddTicks(1592), "$2a$11$nsEfvRG1lsys1loZJ3g.UuQnDpk50aXF9DKVZqcwu8KPi/6yAjHzu" });
        }
    }
}
