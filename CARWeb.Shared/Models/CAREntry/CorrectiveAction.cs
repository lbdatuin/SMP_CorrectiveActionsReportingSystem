using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class CorrectiveAction
    {
        [Key]
        public int Id { get; set; }
        public List<CorrectiveActionItem> CorrectiveActionItems { get; set; }
        public string PersonResponsible { get; set; } = string.Empty;
        public string DepartmentHead { get; set; } = string.Empty;
        public string ReviewedBy { get; set; } = string.Empty;
        public string ReviewerDesignation { get; set; } = string.Empty;
        public DateTime ReviewedDate { get; set; } = DateTime.Now;
        public List<string> InternalCommunicationFiles { get; set; } = new List<string>();
        public bool IsManagementOfChange { get; set; }
        public List<string> ManagementOfChangeFiles { get; set; } = new List<string>();
        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
