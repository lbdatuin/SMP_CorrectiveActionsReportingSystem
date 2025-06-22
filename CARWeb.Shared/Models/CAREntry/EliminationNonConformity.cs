using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Models.DepartmentSection;

namespace CARWeb.Shared.Models.CAREntry
{
    public class EliminationNonConformity
    {
        [Key]
        public int Id { get; set; }
        public bool IsSimilarSituation { get; set; } = false;
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public string IsSimilarSituationDescription { get; set; } = string.Empty;
        public List<string> IsSimilarSituationFiles { get; set; } = new List<string>();
        public bool IsWhyWhy { get; set; } = false;
        public bool IsFishBone { get; set; } = false;
        public bool IsFaultTree { get; set; } = false;
        public bool IsOthers { get; set; } = false;
        public List<string> MethodFiles { get; set; } = new List<string>();
        public string IsOthersDescription { get; set; } = string.Empty;
        public string RootCaseDescription { get; set; } = string.Empty;
        public string AnalyzedBy { get; set; } = string.Empty;
        public DateTime AnalyzedDate { get; set; }
        public string WorkerRepresentative { get; set; } = string.Empty;
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? ReviewedBy { get; set; }
        public string? Designation { get; set; }
        public DateTime? ReviewedDate { get; set; }

        public CARHeader CARHeader { get; set; }
        public int CARHeaderId { get; set; }
    }
}
