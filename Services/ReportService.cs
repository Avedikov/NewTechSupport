
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

namespace TechSupport.Services
{
    public static class ReportService
    {
        public static void GenerateTicketReport(List<Ticket> tickets, string filePath)
        {
            using var fs = new FileStream(filePath, FileMode.Create);
            var document = new iTextSharp.text.Document(PageSize.A4.Rotate());
            var writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            // Заголовок
            var title = new iTextSharp.text.Paragraph("Отчет по заявкам",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Дата генерации
            document.Add(new iTextSharp.text.Paragraph($"Дата генерации: {DateTime.Now:dd.MM.yyyy HH:mm}")
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f
            });

            // Таблица
            var table = new PdfPTable(6);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 0.5f, 2f, 1f, 1f, 1.5f, 1f });

            // Заголовки столбцов
            AddHeaderCell(table, "ID");
            AddHeaderCell(table, "Тема");
            AddHeaderCell(table, "Статус");
            AddHeaderCell(table, "Категория");
            AddHeaderCell(table, "Исполнитель");
            AddHeaderCell(table, "Дата создания");

            // Данные
            foreach (var ticket in tickets)
            {
                AddCell(table, ticket.Id.ToString());
                AddCell(table, ticket.Subject);
                AddCell(table, ticket.Status);
                AddCell(table, ticket.Category ?? "-");
                AddCell(table, ticket.AssignedUser?.Name ?? "Не назначен");
                AddCell(table, ticket.CreateDate.ToString("dd.MM.yyyy"));
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
