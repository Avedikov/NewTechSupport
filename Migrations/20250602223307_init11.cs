using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 22, 33, 6, 719, DateTimeKind.Utc).AddTicks(3516), "$2a$11$LtBf8D86vlWVbcuhNE5J0uqK5S9ZrNzoCbzvg2rdc1r7HlvXTz.pq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 22, 33, 6, 898, DateTimeKind.Utc).AddTicks(9265), "$2a$11$iDcybSxuko9g3Bex0hIkFupeJ6/y552f/L.Lzq9U6H2Xf4ibupgbC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 21, 28, 50, 663, DateTimeKind.Utc).AddTicks(4227), "$2a$11$NzTEz/IyFC17p/ls4MvrFOoUoLHbMAB3q9rBK91t1GCWo/g/.RWbu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 2, 21, 28, 50, 852, DateTimeKind.Utc).AddTicks(4480), "$2a$11$9rKiqKW3dlBEdJYkxadAsuGQePUQEf/a75WU0SdxKAaFZSDnxpXem" });
        }
    }
}
