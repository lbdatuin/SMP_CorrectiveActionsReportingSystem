using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Enums;
using CARWeb.Shared.Models.CAREntry;
using CARWeb.Shared.Models.CARLabel;
using CARWeb.Shared.Models.DepartmentSection;

namespace CARWeb.Shared.DTOs.CAREntryDTO
{
    public class CreateCARHeaderDTO
    {
        public string SysRefNo { get; set; } = string.Empty;
        public string CARNo { get; set; } = string.Empty;
        public int? RevisionNo { get; set; }
        public DateTime? RevisionDate { get; set; }
        public bool Recurring { get; set; } = false;
        public bool NonRecurring { get; set; } = false;
        public List<CreateStandardItem> StandardItems { get; set; }
        public string IssuedTo { get; set; } = string.Empty;
        public string IssuedBy { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        public string Clauses { get; set; } = string.Empty;

        //NON CONFORMITY 
        public List<CreateNonConformityItem> NonConformityItems { get; set; }
        public int? CARTypeId { get; set; }
        public string TypeOfFinding { get; set; } = string.Empty;
        public string TypeOfAccident { get; set; } = string.Empty;

        public CARStatus Status { get; set; }

        //RELATIONS
        public CreateDetailsOfIssue DetailsOfIssue { get; set; } = new CreateDetailsOfIssue();
        public CreateImmediateCorrection ImmediateCorrection { get; set; } = new CreateImmediateCorrection();
        public CreateEliminationNonConformity EliminationNonConformity { get; set; } = new CreateEliminationNonConformity();
    }

    public class CreateStandardItem
    {
        public int CARHeaderId { get; set; }
        public int StandardId { get; set; }
    }

    public class CreateNonConformityItem
    {
        public int NonConformityId { get; set; }
        public int CARHeaderId { get; set; }
    }

    public class CreateDetailsOfIssue
    {
        public string DetailsOfIssueDescription { get; set; } = string.Empty;
        public List<string> DetailsOfIssueFiles { get; set; } = new List<string>();

        public string EvidenceDescription { get; set; } = string.Empty;
        public List<string> EvidenceFiles { get; set; } = new List<string>();

        public string RequirementsDescription { get; set; } = string.Empty;
        public List<string> RequirementsFiles { get; set; } = new List<string>();
        public int CARHeaderId { get; set; }
    }

    public class CreateImmediateCorrection
    {
        public string ActionsToCorrectDescription { get; set; } = string.Empty;
        public List<string> ActionsToCorrectFiles { get; set; } = new List<string>();

        public string ActionsToDealDescription { get; set; } = string.Empty;
        public List<string> ActionsToDealFiles { get; set; } = new List<string>();
        public int CARHeaderId { get; set; }
    }

    public class CreateEliminationNonConformity
    {
        public bool IsSimilarSituation { get; set; } = false;
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
        public string? ReviewedBy { get; set; }
        public string? Designation { get; set; }
        public DateTime? ReviewedDate { get; set; }

        public int CARHeaderId { get; set; }
    }

}
