using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
