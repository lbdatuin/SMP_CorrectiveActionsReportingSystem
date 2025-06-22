using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class CorrectiveActionItem
    {
        [Key]
        public int Id { get; set; }
        public string CAction { get; set; } = string.Empty;
        public string Responsible { get; set; } = string.Empty;
        public DateTime CompletionDate { get; set; } = DateTime.Now;
        public CorrectiveAction CorrectiveAction { get; set; } 
        public int CorrectiveActionId { get; set; }
    }
}
