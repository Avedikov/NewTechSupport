using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Tickets_SourceTicketId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AssignedUserId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AuthorId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AuthorId",
                table: "Tickets");

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
                name: "FK_KnowledgeArticles_Tickets_SourceTicketId",
                table: "KnowledgeArticles",
                column: "SourceTicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeArticles_Users_AssignedUserId",
                table: "KnowledgeArticles",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeArticles_Users_AuthorId",
                table: "KnowledgeArticles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_KnowledgeArticles_KnowledgeArticleId",
                table: "Tickets",
                column: "KnowledgeArticleId",
                principalTable: "KnowledgeArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AuthorId",
                table: "Tickets",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Tickets_SourceTicketId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AssignedUserId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AuthorId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_KnowledgeArticles_KnowledgeArticleId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AuthorId",
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
                values: new object[] { new DateTime(2025, 6, 5, 18, 39, 3, 59, DateTimeKind.Utc).AddTicks(9721), "$2a$11$kn2LGSHgXHyNSF4Ec9g5ruyAeoG8OBMdqaKkngxQ.OHTo1X8.Cdb2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 5, 18, 39, 3, 213, DateTimeKind.Utc).AddTicks(4433), "$2a$11$.nFW0WslFJcy55MKmryxrOphClpF/BAZ/.PeTfzHnJPUOT7VtOopS" });

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeArticles_Tickets_SourceTicketId",
                table: "KnowledgeArticles",
                column: "SourceTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeArticles_Users_AssignedUserId",
                table: "KnowledgeArticles",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KnowledgeArticles_Users_AuthorId",
                table: "KnowledgeArticles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AuthorId",
                table: "Tickets",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
