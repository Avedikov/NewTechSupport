using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        
        //public string Salt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User"; // User, TechSupport, Admin

        [MaxLength(100)]
        public string Position { get; set; }

        [MaxLength(100)]
        public string Department { get; set; }

        public bool IsActive { get; set; } = true;

        // Вычисляемые свойства для удобства
        [NotMapped]
        public bool IsAdmin => Role == "Admin";

        [NotMapped]
        public bool IsTechSupport => Role == "TechSupport" || IsAdmin;

        // Навигационные свойства
        public virtual ICollection<Ticket> AssignedTickets { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }

        public User()
        {
            AssignedTickets = new HashSet<Ticket>();
            TicketHistories = new HashSet<TicketHistory>();
        }
    }
}
