using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AssignedUserId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AuthorId",
                table: "KnowledgeArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "ArticleHistory");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "KnowledgeArticles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "KnowledgeArticles");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TicketHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "KnowledgeArticles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KnowledgeArticles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "SourceTicketId",
                table: "KnowledgeArticles",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AuthorId",
                table: "Tickets",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeArticles_SourceTicketId",
                table: "KnowledgeArticles",
                column: "SourceTicketId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
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
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AuthorId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AuthorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeArticles_SourceTicketId",
                table: "KnowledgeArticles");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TicketHistories");

            migrationBuilder.DropColumn(
                name: "SourceTicketId",
                table: "KnowledgeArticles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "KnowledgeArticles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KnowledgeArticles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "KnowledgeArticles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "KnowledgeArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ArticleHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleHistory_KnowledgeArticles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "KnowledgeArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleHistory_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 3, 21, 51, 11, 822, DateTimeKind.Utc).AddTicks(935), "$2a$11$ewt4TbbQVWG02YL240TpluqpLvFYQkeFxlUEpiDPlanXrteAUKfIa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 3, 21, 51, 11, 985, DateTimeKind.Utc).AddTicks(9076), "$2a$11$86pwTDnUFXXOJcT8PGxG9O1ZJtfJCmMyoWm18knZF2FrLCLpkXy1m" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHistory_ArticleId",
                table: "ArticleHistory",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleHistory_ChangedByUserId",
                table: "ArticleHistory",
                column: "ChangedByUserId");

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
                name: "FK_Tickets_Users_AssignedUserId",
                table: "Tickets",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
