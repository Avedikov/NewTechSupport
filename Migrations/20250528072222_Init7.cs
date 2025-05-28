using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 7, 22, 21, 941, DateTimeKind.Utc).AddTicks(5530), "$2a$11$VW2g1EM4TLXx/Q9..Ye3VONybICD1Ov0pDvI9FXYpCLYnog/xQGkS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 7, 22, 22, 121, DateTimeKind.Utc).AddTicks(44), "$2a$11$1QWixwygXoCC6IRKDshCt.5ucqUtrkkPAdGH1zoruvb9K8C8aas6O" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
