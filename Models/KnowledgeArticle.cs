using System.ComponentModel.DataAnnotations.Schema;

namespace TechSupport.Models
{
    public class KnowledgeArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "Черновик";

        // Связи
        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        [NotMapped]
        public string Description { get; set; }

        public int? AssignedUserId { get; set; }
        [ForeignKey("AssignedUserId")]
        public virtual User AssignedUser { get; set; }

        // Исправленная связь с Ticket
        [NotMapped]
        public int? SourceTicketId { get; set; }
        [ForeignKey("SourceTicketId")]
        public virtual Ticket SourceTicket { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
