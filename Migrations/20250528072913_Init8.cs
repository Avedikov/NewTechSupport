using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class Init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2025, 5, 28, 7, 22, 21, 941, DateTimeKind.Utc).AddTicks(5530), "$2a$11$VW2g1EM4TLXx/Q9..Ye3VONybICD1Ov0pDvI9FXYpCLYnog/xQGkS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 5, 28, 7, 22, 22, 121, DateTimeKind.Utc).AddTicks(44), "$2a$11$1QWixwygXoCC6IRKDshCt.5ucqUtrkkPAdGH1zoruvb9K8C8aas6O" });
        }
    }
}
