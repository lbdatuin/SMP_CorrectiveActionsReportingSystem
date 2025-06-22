using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class IMVerification
    {
        [Key]
        public int Id { get; set; }
        public bool IsQA { get; set; }
        public string QAReason { get; set; } = string.Empty;
        public bool IsQB { get; set; }
        public string QBReason { get; set; } = string.Empty;
        public bool IsQC { get; set; }
        public string QCReason { get; set; } = string.Empty;
        public bool IsQD { get; set; }
        public string QDReason { get; set; } = string.Empty;
        public bool IsQE { get; set; }
        public string QEReason { get; set; } = string.Empty;
        public string CourseOfAction { get; set; } = string.Empty;
        public string CheckedBy { get; set; } = string.Empty;
        public DateTime CheckedDate { get; set; } = DateTime.Now;
        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
