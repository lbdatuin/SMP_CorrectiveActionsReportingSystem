using System.Security.Claims;
using CARWeb.Data;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.Enums;
using CARWeb.Shared.Models.CAREntry;
using CARWeb.Shared.Models.CARLabel;
using CARWeb.Shared.Models.DepartmentSection;

namespace CARWeb.Services.CAREntryService
{
    public class CAREntryService : ICAREntryService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CAREntryService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<int> CreateEntry(CreateCARHeaderDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                CARHeader payload = new CARHeader
                {
                    SysRefNo = request.SysRefNo,
                    CARNo = request.CARNo,
                    Recurring = request.Recurring,
                    NonRecurring = request.NonRecurring,
                    IssuedTo = request.IssuedTo,
                    IssuedBy = request.IssuedBy,
                    IssuanceDate = request.IssuedDate,
                    Clauses = request.Clauses,
                    CARTypeId = request.CARTypeId,
                    TypeOfFinding = request.TypeOfFinding,
                    TypeOfAccident = request.TypeOfAccident,
                    Status = CARStatus.OPEN,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                    DetailsOfIssue = new DetailsOfIssue
                    {
                       DetailsOfIssueDescription = request.DetailsOfIssue.DetailsOfIssueDescription,
                       DetailsOfIssueFiles = request.DetailsOfIssue.DetailsOfIssueFiles,
                       EvidenceDescription = request.DetailsOfIssue.EvidenceDescription,
                       EvidenceFiles = request.DetailsOfIssue.EvidenceFiles,
                       RequirementsDescription = request.DetailsOfIssue.RequirementsDescription,
                       RequirementsFiles = request.DetailsOfIssue.RequirementsFiles,
                    },
                    ImmediateCorrection = new ImmediateCorrection
                    {
                        ActionsToCorrectDescription = request.ImmediateCorrection.ActionsToCorrectDescription,
                        ActionsToCorrectFiles = request.ImmediateCorrection.ActionsToCorrectFiles,
                        ActionsToDealDescription = request.ImmediateCorrection.ActionsToDealDescription,
                        ActionsToDealFiles = request.ImmediateCorrection.ActionsToDealFiles,
                    },
                    EliminationNonConformity = new EliminationNonConformity
                    {
                        IsSimilarSituation = request.EliminationNonConformity.IsSimilarSituation,
                        DepartmentId = request.EliminationNonConformity.DepartmentId,
                        IsSimilarSituationDescription = request.EliminationNonConformity.IsSimilarSituationDescription,
                        IsSimilarSituationFiles = request.EliminationNonConformity.IsSimilarSituationFiles,
                        IsWhyWhy = request.EliminationNonConformity.IsWhyWhy,
                        IsFishBone = request.EliminationNonConformity.IsFishBone,
                        IsFaultTree = request.EliminationNonConformity.IsFaultTree,
                        IsOthers = request.EliminationNonConformity.IsOthers,
                        MethodFiles = request.EliminationNonConformity.MethodFiles,
                        IsOthersDescription = request.EliminationNonConformity.IsOthersDescription,
                        RootCaseDescription = request.EliminationNonConformity.RootCaseDescription,
                        AnalyzedBy = request.EliminationNonConformity.AnalyzedBy,
                        AnalyzedDate = request.EliminationNonConformity.AnalyzedDate,
                        WorkerRepresentative = request.EliminationNonConformity.WorkerRepresentative,
                        ReviewedBy = request.EliminationNonConformity.ReviewedBy,
                        Designation = request.EliminationNonConformity.Designation,
                        ReviewedDate = request.EliminationNonConformity.ReviewedDate,
                    }
                };

                foreach (var item in request.StandardItems)
                {
                    StandardItem standard_item = new StandardItem
                    {
                        CARHeader = payload,
                        CARHeaderId = payload.Id,
                        StandardId = item.StandardId,
                    };
                    _context.StandardItems.Add(standard_item);
                }

                foreach (var item in request.NonConformityItems)
                {
                    NonConformityItem non_conformity_item = new NonConformityItem
                    {
                        CARHeader = payload,
                        CARHeaderId = payload.Id,
                        NonConformityId = item.NonConformityId,
                    };
                    _context.NonConformityItems.Add(non_conformity_item);
                }

                _context.CARHeaders.Add(payload);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
