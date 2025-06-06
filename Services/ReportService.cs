
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TechSupport.Models;
using Document = iTextSharp.text.Document;

namespace TechSupport.Services
{
    public static class ReportService
    {
        public static void GenerateKnowledgeBaseReport(List<KnowledgeArticle> articles, string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            var document = new Document(PageSize.A4);
            var writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            // Заголовок
            document.Add(new iTextSharp.text.Paragraph("База знаний - Отчет",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)));

            // Таблица с данными
            var table = new PdfPTable(5);
            table.WidthPercentage = 100;

            // Заголовки столбцов
            table.AddCell("ID");
            table.AddCell("Название");
            table.AddCell("Статус");
            table.AddCell("Ответственный");
            table.AddCell("Дата обновления");

            // Данные
            foreach (var article in articles)
            {
                table.AddCell(article.Id.ToString());
                table.AddCell(article.Title);
                table.AddCell(article.Status);
                table.AddCell(article.AssignedUser?.Name ?? "Не назначен");
                table.AddCell(article.LastUpdated.ToString("dd.MM.yyyy"));
            }

            document.Add(table);
            document.Close();
        }

        public static void GenerateUserReport(List<User> users, string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            var document = new iTextSharp.text.Document();
            var writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            // Заголовок
            var title = new iTextSharp.text.Paragraph("Отчет по пользователям",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Таблица
            var table = new PdfPTable(5);
            table.WidthPercentage = 100;

            // Заголовки столбцов
            AddHeaderCell(table, "ID");
            AddHeaderCell(table, "Логин");
            AddHeaderCell(table, "Имя");
            AddHeaderCell(table, "Роль");
            AddHeaderCell(table, "Активен");

            // Данные
            foreach (var user in users)
            {
                AddCell(table, user.Id.ToString());
                AddCell(table, user.Username);
                AddCell(table, user.Name);
                AddCell(table, user.Role);
                AddCell(table, user.IsActive ? "Да" : "Нет");
            }

            document.Add(table);
            document.Close();
        }

        private static void AddHeaderCell(PdfPTable table, string text)
        {
            var cell = new PdfPCell(new Phrase(text,
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD)));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 5;
            table.AddCell(cell);
        }

        private static void AddCell(PdfPTable table, string text)
        {
            var cell = new PdfPCell(new Phrase(text));
            cell.Padding = 5;
            table.AddCell(cell);
        }
    }
}
