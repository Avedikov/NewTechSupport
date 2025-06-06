using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class updateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
