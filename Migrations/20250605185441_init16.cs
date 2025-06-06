using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_KnowledgeArticles_KnowledgeArticleId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_KnowledgeArticleId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "KnowledgeArticleId",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 5, 18, 54, 40, 736, DateTimeKind.Utc).AddTicks(4002), "$2a$11$oPiHDmDGJkqg93GwavsPAOqgBGL6K1PxSs.az/FN7kXAvr8rnQ3wm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 5, 18, 54, 40, 902, DateTimeKind.Utc).AddTicks(9090), "$2a$11$l/NLvrd9cDzfYbSy6.vHj.Krowr/qiHwZYbwFaqMoky33jpy.2ADi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KnowledgeArticleId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 5, 18, 40, 44, 52, DateTimeKind.Utc).AddTicks(7307), "$2a$11$f/wzPGJBYZgoBLcai9ZRNucjPWFsEbRXaD63xeySwb4EkmcRk3oKS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 5, 18, 40, 44, 206, DateTimeKind.Utc).AddTicks(9677), "$2a$11$shPjSVX9iEElLu2ddYFgFOlFuELlRAWerWv.OF8LqMUK81n2xcrkq" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_KnowledgeArticleId",
                table: "Tickets",
                column: "KnowledgeArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_KnowledgeArticles_KnowledgeArticleId",
                table: "Tickets",
                column: "KnowledgeArticleId",
                principalTable: "KnowledgeArticles",
                principalColumn: "Id");
        }
    }
}
