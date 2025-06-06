using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistories_KnowledgeArticles_KnowledgeArticleId",
                table: "TicketHistories");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistories_KnowledgeArticleId",
                table: "TicketHistories");

            migrationBuilder.DropColumn(
                name: "KnowledgeArticleId",
                table: "TicketHistories");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KnowledgeArticleId",
                table: "TicketHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 7, 29, 12, 567, DateTimeKind.Utc).AddTicks(6689), "$2a$11$xP1SLhgHcYLryInZ6bZ1hOhT1aloLbUsc1iGR0qk3gHJECatHhMvq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 7, 29, 12, 749, DateTimeKind.Utc).AddTicks(4441), "$2a$11$1m6RtXnuyesLJPi0.1hWtuMUozRahQQBwlOmh6y04Tp2262/EsII2" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistories_KnowledgeArticleId",
                table: "TicketHistories",
                column: "KnowledgeArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistories_KnowledgeArticles_KnowledgeArticleId",
                table: "TicketHistories",
                column: "KnowledgeArticleId",
                principalTable: "KnowledgeArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
