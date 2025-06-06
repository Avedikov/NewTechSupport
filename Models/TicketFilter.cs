using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.Models
{
    public class TicketFilter
    {
        public string? Status { get; set; }
        public int? AssignedUserId { get; set; }
        public string? SearchText { get; set; } // Для поиска по фамилии/email/теме
        public string ClientNameSearch { get; set; }
        public string SubjectSearch { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateFilterType DateFilterType { get; set; } = DateFilterType.CreateDate;

        
    }

    public enum DateFilterType
    {
        CreateDate,
        LastUpdateDate
    }

}
