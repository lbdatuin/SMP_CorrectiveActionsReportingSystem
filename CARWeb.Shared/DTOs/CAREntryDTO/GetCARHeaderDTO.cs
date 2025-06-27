using CARWeb.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARWeb.Shared.DTOs.CAREntryDTO
{
    public class GetCARHeaderDTO
    {
        public int Id { get; set; }
        public string SysRefNo { get; set; } = string.Empty;
        public string CARNo { get; set; } = string.Empty;
        public int? RevisionNo { get; set; }
        public DateTime? RevisionDate { get; set; }
        public bool Recurring { get; set; } = false;
        public bool NonRecurring { get; set; } = false;
        public List<GetStandardItem> StandardItems { get; set; }
        public string IssuedTo { get; set; } = string.Empty;
        public string IssuedBy { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; } = DateTime.Now;
        public string Clauses { get; set; } = string.Empty;

        //NON CONFORMITY 
        public List<GetNonConformityItem> NonConformityItems { get; set; }
        public int? CARTypeId { get; set; }
        public string TypeOfFinding { get; set; } = string.Empty;
        public string TypeOfAccident { get; set; } = string.Empty;

        public CARStatus Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public List<GetReturnComment> ReturnComments { get; set; } = new List<GetReturnComment>();

        //RELATIONS
        public GetIssuedBy CARIssuedBy { get; set; } = new GetIssuedBy();
        public GetIssuedTo CARIssuedTo { get; set; } = new GetIssuedTo();
        public GetDetailsOfIssue DetailsOfIssue { get; set; } = new GetDetailsOfIssue();
        public GetImmediateCorrection ImmediateCorrection { get; set; } = new GetImmediateCorrection();
        public GetEliminationNonConformity EliminationNonConformity { get; set; } = new GetEliminationNonConformity();
        public GetCorrectiveAction CorrectiveAction { get; set; } = new GetCorrectiveAction();
        public GetIMVerification IMVerification { get; set; } = new GetIMVerification();
        public GetFollowUpStatus FollowUpStatus { get; set; } = new GetFollowUpStatus();
        public GetStatusOfEffectiveness StatusOfEffectiveness { get; set; } = new GetStatusOfEffectiveness();
    }

    public class GetIssuedBy
    {
        public int DepartmentId { get; set; }
        public List<GetIssuedByItems> IssuedByItems { get; set; } = new List<GetIssuedByItems>();
    }

    public class GetIssuedByItems
    {
        public int DSectionId { get; set; }
    }

    public class GetIssuedTo
    {
        public int DepartmentId { get; set; }
        public List<GetIssuedToItems> IssuedToItems { get; set; } = new List<GetIssuedToItems>();
    }

    public class GetIssuedToItems
    {
        public int DSectionId { get; set; }
    }

    public class GetReturnComment
    {
        public string From { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime ReturnedDate { get; set; }
        public int CARHeaderId { get; set; }
    }

    public class GetStandardItem
    {
        public int CARHeaderId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int StandardId { get; set; }
    }

    public class GetNonConformityItem
    {
        public int NonConformityId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int CARHeaderId { get; set; }
    }

    public class GetDetailsOfIssue
    {
        public string DetailsOfIssueDescription { get; set; } = string.Empty;
        public List<string> DetailsOfIssueFiles { get; set; } = new List<string>();

        public string EvidenceDescription { get; set; } = string.Empty;
        public List<string> EvidenceFiles { get; set; } = new List<string>();

        public string RequirementsDescription { get; set; } = string.Empty;
        public List<string> RequirementsFiles { get; set; } = new List<string>();
        public int CARHeaderId { get; set; }
    }

    public class GetImmediateCorrection
    {
        public string ActionsToCorrectDescription { get; set; } = string.Empty;
        public List<string> ActionsToCorrectFiles { get; set; } = new List<string>();

        public string ActionsToDealDescription { get; set; } = string.Empty;
        public List<string> ActionsToDealFiles { get; set; } = new List<string>();
        public int CARHeaderId { get; set; }
    }

    public class GetEliminationNonConformity
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
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string WorkerRepresentative { get; set; } = string.Empty;
        public string? ReviewedBy { get; set; }
        public string? Designation { get; set; }
        public DateTime? ReviewedDate { get; set; }

        public int CARHeaderId { get; set; }
    }

    public class GetCorrectiveAction
    {
        public List<GetCorrectiveActionItem> CorrectiveActionItems { get; set; } = new List<GetCorrectiveActionItem>();
        public string PersonResponsible { get; set; } = string.Empty;
        public string DepartmentHead { get; set; } = string.Empty;
        public string ReviewedBy { get; set; } = string.Empty;
        public string ReviewerDesignation { get; set; } = string.Empty;
        public DateTime ReviewedDate { get; set; } = DateTime.Now;
        public List<string> InternalCommunicationFiles { get; set; } = new List<string>();
        public bool IsManagementOfChange { get; set; }
        public List<string> ManagementOfChangeFiles { get; set; } = new List<string>();
        public int CARHeaderId { get; set; }
    }

    public class GetCorrectiveActionItem
    {
        public string CAction { get; set; } = string.Empty;
        public string Responsible { get; set; } = string.Empty;
        public DateTime CompletionDate { get; set; } = DateTime.Now;
        public int CorrectiveActionId { get; set; }
    }

    public class GetIMVerification
    {
        public bool IsQA { get; set; } = true;
        public string QAReason { get; set; } = string.Empty;
        public bool IsQB { get; set; } = true;
        public string QBReason { get; set; } = string.Empty;
        public bool IsQC { get; set; } = true;
        public string QCReason { get; set; } = string.Empty;
        public bool IsQD { get; set; } = true;
        public string QDReason { get; set; } = string.Empty;
        public bool IsQE { get; set; } = true;
        public string QEReason { get; set; } = string.Empty;
        public string CourseOfAction { get; set; } = string.Empty;
        public string CheckedBy { get; set; } = string.Empty;
        public int CARHeaderId { get; set; }
    }

    public class GetFollowUpStatus
    {
        public DateTime F1Date { get; set; } = DateTime.Now;
        public List<string> F1Evidences { get; set; } = new List<string>();
        public string F1StatusOfActions { get; set; } = string.Empty;
        public string F1VerifiedBy { get; set; } = string.Empty;

        public DateTime F2Date { get; set; } = DateTime.Now;
        public List<string> F2Evidences { get; set; } = new List<string>();
        public string F2StatusOfActions { get; set; } = string.Empty;
        public string F2VerifiedBy { get; set; } = string.Empty;

        public DateTime F3Date { get; set; } = DateTime.Now;
        public List<string> F3Evidences { get; set; } = new List<string>();
        public string F3StatusOfActions { get; set; } = string.Empty;
        public string F3VerifiedBy { get; set; } = string.Empty;

        public int CARHeaderId { get; set; }
    }

    public class GetStatusOfEffectiveness
    {
        public bool IsS1 { get; set; } = false;
        public bool IsS2 { get; set; } = false;
        public bool IsS3 { get; set; } = false;
        public string VerifiedBy { get; set; } = string.Empty;
        public string NotedBy { get; set; } = string.Empty;
        public int CARHeaderId { get; set; }
    }
}
