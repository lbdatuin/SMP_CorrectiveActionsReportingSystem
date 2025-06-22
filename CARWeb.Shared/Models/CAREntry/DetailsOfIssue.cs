using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.Models.CAREntry
{
    public class DetailsOfIssue
    {
        [Key]
        public int Id { get; set; }

        public string DetailsOfIssueDescription { get; set; } = string.Empty;
        public List<string> DetailsOfIssueFiles { get; set; } = new List<string>();

        public string EvidenceDescription { get; set; } = string.Empty;
        public List<string> EvidenceFiles { get; set; } = new List<string>();

        public string RequirementsDescription { get; set; } = string.Empty;
        public List<string> RequirementsFiles { get; set; } = new List<string>();

        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
