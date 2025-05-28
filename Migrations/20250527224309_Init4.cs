using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 43, 8, 892, DateTimeKind.Utc).AddTicks(4732), "$2a$11$B9Fr2XfukdviR0yPJ6Pj2.cZGOPVklcwt4lE37IVwiUavba1KIexK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 27, 22, 43, 9, 69, DateTimeKind.Utc).AddTicks(6231), "$2a$11$S7ikHA3evdAZOt8aNERr9uZKpGNwjRW8sKLthxE7XBF2weKRu9Osa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
