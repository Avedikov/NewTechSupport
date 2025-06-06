using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.Models;
using System.Reflection;

namespace TechSupport.Services
{
    public interface IPdfService
    {
        Task GenerateTicketsReportAsync(List<Ticket> tickets, string filePath);
    }

   public class PdfService : IPdfService
    {
        public async Task GenerateTicketsReportAsync(List<Ticket> tickets, string filePath)
        {
            await Task.Run(() =>
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                    var writer = PdfWriter.GetInstance(document, stream);

                    document.Open();

                    // Добавляем заголовок
                    var title = new Paragraph("Отчет по заявкам",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Добавляем дату генерации
                    document.Add(new Paragraph($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}"));
                    document.Add(Chunk.NEWLINE);

                    // Создаем таблицу
                    var table = new PdfPTable(7)
                    {
                        WidthPercentage = 100,
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };

                    // Заголовки столбцов
                    table.AddCell("ID");
                    table.AddCell("Тема");
                    table.AddCell("Фамилия");
                    table.AddCell("Статус");
                    table.AddCell("Дата создания");
                    table.AddCell("Дата обновления");
                    table.AddCell("Ответственный");

                    // Данные
                    foreach (var ticket in tickets)
                    {
                        table.AddCell(ticket.Id.ToString());
                        table.AddCell(ticket.Subject);
                        table.AddCell(ticket.ClientName);
                        table.AddCell(ticket.Status);
                        table.AddCell(ticket.CreateDate.ToString("dd.MM.yyyy"));
                        table.AddCell(ticket.LastUpdated.ToString("dd.MM.yyyy"));
                        table.AddCell(ticket.AssignedUser?.Name ?? "Не назначен");
                    }

                    document.Add(table);
                    document.Close();
                }
            });
        }
    }
}

