using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.Models
{
    public class TicketHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TicketId { get; set; }

        [Required]
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
        

    [Required]
        [StringLength(50)]
        public string Action { get; set; } // "StatusChanged", "Assigned", "Created"

        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        [Required]
        [StringLength(50)]
        public string ChangedBy { get; set; }

        public int? ChangedByUserId { get; set; }
        [ForeignKey("ChangedByUserId")]
        public virtual User? ChangedByUser { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        
    }
}
