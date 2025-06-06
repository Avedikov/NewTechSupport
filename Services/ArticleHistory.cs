using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.Models;

namespace TechSupport.Services
{
    public class ArticleHistory
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }
        public KnowledgeArticle Article { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; } // "StatusChanged", "Assigned", "Created"

        public string OldValue { get; set; }
        public string NewValue { get; set; }

        [Required]
        public int ChangedByUserId { get; set; }
        public User ChangedByUser { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
