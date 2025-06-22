using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CARWeb.Shared.Enums;
using CARWeb.Shared.Models.CARLabel;

namespace CARWeb.Shared.Models.CAREntry
{
    public class CARHeader
    {
        [Key]
        public int Id { get; set; }
        public string SysRefNo { get; set; } = string.Empty;
        public string CARNo { get; set; } = string.Empty;
        public int? RevisionNo { get; set; }
        public DateTime? RevisionDate { get; set; }
        public bool Recurring { get; set; } = false;
        public bool NonRecurring { get; set; } = false;
        public List<StandardItem> StandardItems { get; set; }
        public string IssuedTo { get; set; } = string.Empty;
        public string IssuedBy { get; set; } = string.Empty;
        public DateTime IssuanceDate { get; set; } = DateTime.Now;
        public string Clauses { get; set; } = string.Empty;

        //NON CONFORMITY 
        public List<NonConformityItem> NonConformityItems { get; set; }
        public CARType? CARType { get; set; }
        public int? CARTypeId { get; set; }
        public string TypeOfFinding { get; set; } = string.Empty;
        public string TypeOfAccident { get; set; } = string.Empty;

        public CARStatus Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }

        //RELATIONS
        public DetailsOfIssue DetailsOfIssue { get; set; }
        public ImmediateCorrection ImmediateCorrection { get; set; }
        public EliminationNonConformity EliminationNonConformity { get; set; }
        public CorrectiveAction CorrectiveAction { get; set; }
        public IMVerification IMVerification { get; set; }
        public FollowUpStatus FollowUpStatus { get; set; }
        public StatusOfEffectiveness StatusOfEffectiveness { get; set; }
    }
}
