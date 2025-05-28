using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Новая";

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public string? AttachedFilePath { get; set; }

        [StringLength(100)]
        public string? ClientName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        // Внешний ключ и навигационное свойство для исполнителя
        public int? AssignedUserId { get; set; }
        [ForeignKey("AssignedUserId")]
        public virtual User? AssignedUser { get; set; }

        // История изменений
        public virtual ICollection<TicketHistory> History { get; set; }

        public Ticket()
        {
            History = new HashSet<TicketHistory>();
        }
    }
}
