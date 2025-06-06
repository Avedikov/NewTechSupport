using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechSupport.Migrations
{
    /// <inheritdoc />
    public partial class init13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KnowledgeArticles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "KnowledgeArticles",
                type: "int",
                nullable: true);

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
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_KnowledgeArticles_AssignedUserId",
                table: "KnowledgeArticles",
                column: "AssignedUserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnowledgeArticles_Users_AssignedUserId",
                table: "KnowledgeArticles");

            migrationBuilder.DropTable(
                name: "ArticleHistory");

            migrationBuilder.DropIndex(
                name: "IX_KnowledgeArticles_AssignedUserId",
                table: "KnowledgeArticles");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "KnowledgeArticles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "KnowledgeArticles");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "KnowledgeArticles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 3, 21, 48, 5, 593, DateTimeKind.Utc).AddTicks(6922), "$2a$11$RBmv4vMewCglKgz37/J2funVWjW8UXiKcsQHcwKrYFpLw.P1aeVvS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 3, 21, 48, 5, 757, DateTimeKind.Utc).AddTicks(2133), "$2a$11$VCImooenYPMKiGQRoqJ/mOgegGNVDYF.0/qsH7h6VE0qKAsDx4gnG" });
        }
    }
}
