using System.Reflection.PortableExecutable;
using System.Security.Claims;
using Azure;
using Azure.Core;
using CARWeb.Data;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Enums;
using CARWeb.Shared.Models.Auth;
using CARWeb.Shared.Models.CAREntry;
using CARWeb.Shared.Models.CAREntry.EntryUser;
using CARWeb.Shared.Models.CARLabel;
using CARWeb.Shared.Models.DepartmentSection;
using CARWeb.Shared.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

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
                    IssuanceDate = request.IssuedDate,
                    Clauses = request.Clauses,
                    CARTypeId = request.CARTypeId,
                    TypeOfFinding = request.TypeOfFinding,
                    TypeOfAccident = request.TypeOfAccident,
                    Status = CARStatus.OPEN,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                    IssuedBy = new IssuedBy
                    {
                        DepartmentId = request.CARIssuedBy.DepartmentId,
                        IssuedByItems = request.CARIssuedBy.IssuedByItems.Select(item => new IssuedByItem
                        {
                            DSectionId = item.DSectionId
                        }).ToList()
                    },
                    IssuedTo = new IssuedTo
                    {
                        DepartmentId = request.CARIssuedTo.DepartmentId,
                        IssuedToItems = request.CARIssuedTo.IssuedToItems.Select(item => new IssuedToItem
                        {
                            DSectionId = item.DSectionId
                        }).ToList()
                    },
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
                        ApprovedBy = request.EliminationNonConformity.ApprovedBy,
                        ApprovalDate = request.EliminationNonConformity.ApprovalDate,
                        WorkerRepresentative = request.EliminationNonConformity.WorkerRepresentative,
                        ReviewedBy = request.EliminationNonConformity.ReviewedBy,
                        Designation = request.EliminationNonConformity.Designation,
                        ReviewedDate = request.EliminationNonConformity.ReviewedDate,
                    },
                    CorrectiveAction = new CorrectiveAction
                    {
                        PersonResponsible = request.CorrectiveAction.PersonResponsible, 
                        DepartmentHead = request.CorrectiveAction.DepartmentHead,
                        ReviewedBy = request.CorrectiveAction.ReviewedBy,
                        ReviewerDesignation = request.CorrectiveAction.ReviewerDesignation,
                        ReviewedDate = request.CorrectiveAction.ReviewedDate,
                        InternalCommunicationFiles = request.CorrectiveAction.InternalCommunicationFiles,
                        IsManagementOfChange = request.CorrectiveAction.IsManagementOfChange,
                        ManagementOfChangeFiles = request.CorrectiveAction.ManagementOfChangeFiles,
                        CorrectiveActionItems = request.CorrectiveAction.CorrectiveActionItems.Select(item => new CorrectiveActionItem
                        {
                            CAction = item.CAction,
                            Responsible = item.Responsible,
                            CompletionDate = item.CompletionDate,
                            CorrectiveActionId = item.CorrectiveActionId
                        }).ToList()
                    },
                    IMVerification = new IMVerification
                    {
                        IsQA = request.IMVerification.IsQA,
                        QAReason = request.IMVerification.QAReason,
                        IsQB = request.IMVerification.IsQB,
                        QBReason = request.IMVerification.QBReason,
                        IsQC = request.IMVerification.IsQC,
                        QCReason = request.IMVerification.QCReason,
                        IsQD = request.IMVerification.IsQD,
                        QDReason = request.IMVerification.QDReason,
                        IsQE = request.IMVerification.IsQE,
                        QEReason = request.IMVerification.QEReason,
                        CourseOfAction = request.IMVerification.CourseOfAction,
                        CheckedBy = request.IMVerification.CheckedBy
                    },
                    FollowUpStatus = new FollowUpStatus
                    {
                        F1Date = request.FollowUpStatus.F1Date,
                        F1Evidences = request.FollowUpStatus.F1Evidences,
                        F1StatusOfActions = request.FollowUpStatus.F1StatusOfActions,
                        F1VerifiedBy = request.FollowUpStatus.F1VerifiedBy,
                        F2Date = request.FollowUpStatus.F2Date,
                        F2Evidences = request.FollowUpStatus.F2Evidences,
                        F2StatusOfActions = request.FollowUpStatus.F2StatusOfActions,
                        F2VerifiedBy = request.FollowUpStatus.F2VerifiedBy,
                        F3Date = request.FollowUpStatus.F3Date,
                        F3Evidences = request.FollowUpStatus.F3Evidences,
                        F3StatusOfActions = request.FollowUpStatus.F3StatusOfActions,
                        F3VerifiedBy = request.FollowUpStatus.F3VerifiedBy,
                    },
                    StatusOfEffectiveness = new StatusOfEffectiveness
                    {
                        IsS1 = request.StatusOfEffectiveness.IsS1,
                        IsS2 = request.StatusOfEffectiveness.IsS2,
                        IsS3 = request.StatusOfEffectiveness.IsS3,
                        VerifiedBy = request.StatusOfEffectiveness.VerifiedBy,
                        NotedBy = request.StatusOfEffectiveness.NotedBy
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

        private GetCARHeaderDTO ConvertCARHeaderDTO(CARHeader request)
        {
            return new GetCARHeaderDTO
            {
                SysRefNo = string.IsNullOrWhiteSpace(request.SysRefNo) ? "-" : request.SysRefNo,
                CARNo = string.IsNullOrWhiteSpace(request.CARNo) ? "-" : request.CARNo,
                RevisionNo = request.RevisionNo,
                RevisionDate = request.RevisionDate,
                Recurring = request.Recurring,
                NonRecurring = request.NonRecurring,
                IssuedDate = request.IssuanceDate,
                Clauses = request.Clauses ?? "-",
                CARTypeId = request.CARTypeId,
                TypeOfFinding = request.TypeOfFinding ?? "-",
                TypeOfAccident = request.TypeOfAccident ?? "-",
                Status = request.Status,
                CreatedBy = request.CreatedBy ?? "-",
                DateCreated = DateTime.Now,
                
                StandardItems = request.StandardItems?.Select(s => new GetStandardItem
                {
                    StandardId = s.StandardId,
                    Code = s.Standard?.Code ?? "-"
                }).ToList() ?? new List<GetStandardItem>(),

                NonConformityItems = request.NonConformityItems?.Select(n => new GetNonConformityItem
                {
                    NonConformityId = n.NonConformityId,
                    Code = n.NonConformity?.Code ?? "-"
                }).ToList() ?? new List<GetNonConformityItem>(),

                ReturnComments = request.ReturnComments?.Select(r => new GetReturnComment
                {
                    From = r.From,
                    Reason = r.Reason,
                    ReturnedDate = r.ReturnedDate
                }).OrderByDescending(q => q.ReturnedDate).ToList() ?? new List<GetReturnComment>(),

                CARIssuedBy = request.CARIssuedBy == null || request.IssuedBy == null || request.IssuedBy.IssuedByItems == null
                    ? new GetIssuedBy()
                    : new GetIssuedBy
                    {
                        DepartmentId = request.IssuedBy.DepartmentId,
                        IssuedByItems = request.IssuedBy.IssuedByItems
                            .Where(item => item != null)
                            .Select(item => new GetIssuedByItems
                            {
                                DSectionId = item.DSectionId
                            }).ToList()
                    },

                CARIssuedTo = request.CARIssuedTo == null || request.IssuedTo == null || request.IssuedTo.IssuedToItems == null
                    ? new GetIssuedTo()
                    : new GetIssuedTo
                    {
                        DepartmentId = request.IssuedTo.DepartmentId,
                        IssuedToItems = request.IssuedTo.IssuedToItems
                            .Where(item => item != null)
                            .Select(item => new GetIssuedToItems
                            {
                                DSectionId = item.DSectionId
                            }).ToList()
                    },

                DetailsOfIssue = request.DetailsOfIssue == null ? new GetDetailsOfIssue() : new GetDetailsOfIssue
                {
                    DetailsOfIssueDescription = request.DetailsOfIssue.DetailsOfIssueDescription ?? "",
                    DetailsOfIssueFiles = request.DetailsOfIssue.DetailsOfIssueFiles ?? new List<string>(),
                    EvidenceDescription = request.DetailsOfIssue.EvidenceDescription ?? "",
                    EvidenceFiles = request.DetailsOfIssue.EvidenceFiles ?? new List<string>(),
                    RequirementsDescription = request.DetailsOfIssue.RequirementsDescription ?? "",
                    RequirementsFiles = request.DetailsOfIssue.RequirementsFiles ?? new List<string>(),
                },

                ImmediateCorrection = request.ImmediateCorrection == null ? new GetImmediateCorrection() : new GetImmediateCorrection
                {
                    ActionsToCorrectDescription = request.ImmediateCorrection.ActionsToCorrectDescription ?? "",
                    ActionsToCorrectFiles = request.ImmediateCorrection.ActionsToCorrectFiles ?? new List<string>(),
                    ActionsToDealDescription = request.ImmediateCorrection.ActionsToDealDescription ?? "",
                    ActionsToDealFiles = request.ImmediateCorrection.ActionsToDealFiles ?? new List<string>(),
                },

                EliminationNonConformity = request.EliminationNonConformity == null ? new GetEliminationNonConformity() : new GetEliminationNonConformity
                {
                    IsSimilarSituation = request.EliminationNonConformity.IsSimilarSituation,
                    DepartmentId = request.EliminationNonConformity.DepartmentId,
                    IsSimilarSituationDescription = request.EliminationNonConformity.IsSimilarSituationDescription ?? "",
                    IsSimilarSituationFiles = request.EliminationNonConformity.IsSimilarSituationFiles ?? new List<string>(),
                    IsWhyWhy = request.EliminationNonConformity.IsWhyWhy,
                    IsFishBone = request.EliminationNonConformity.IsFishBone,
                    IsFaultTree = request.EliminationNonConformity.IsFaultTree,
                    IsOthers = request.EliminationNonConformity.IsOthers,
                    MethodFiles = request.EliminationNonConformity.MethodFiles ?? new List<string>(),
                    IsOthersDescription = request.EliminationNonConformity.IsOthersDescription ?? "",
                    RootCaseDescription = request.EliminationNonConformity.RootCaseDescription ?? "",
                    AnalyzedBy = request.EliminationNonConformity.AnalyzedBy ?? "",
                    AnalyzedDate = request.EliminationNonConformity.AnalyzedDate,
                    ApprovedBy = request.EliminationNonConformity.ApprovedBy ?? "",
                    ApprovalDate = request.EliminationNonConformity.ApprovalDate,
                    WorkerRepresentative = request.EliminationNonConformity.WorkerRepresentative ?? "",
                    ReviewedBy = request.EliminationNonConformity.ReviewedBy ?? "",
                    Designation = request.EliminationNonConformity.Designation ?? "",
                    ReviewedDate = request.EliminationNonConformity.ReviewedDate
                },

                CorrectiveAction = request.CorrectiveAction == null ? new GetCorrectiveAction() : new GetCorrectiveAction
                {
                    PersonResponsible = request.CorrectiveAction.PersonResponsible ?? "",
                    DepartmentHead = request.CorrectiveAction.DepartmentHead ?? "",
                    ReviewedBy = request.CorrectiveAction.ReviewedBy ?? "",
                    ReviewerDesignation = request.CorrectiveAction.ReviewerDesignation ?? "",
                    ReviewedDate = request.CorrectiveAction.ReviewedDate,
                    InternalCommunicationFiles = request.CorrectiveAction.InternalCommunicationFiles ?? new List<string>(),
                    IsManagementOfChange = request.CorrectiveAction.IsManagementOfChange,
                    ManagementOfChangeFiles = request.CorrectiveAction.ManagementOfChangeFiles ?? new List<string>(),
                    CorrectiveActionItems = request.CorrectiveAction.CorrectiveActionItems?.Select(item => new GetCorrectiveActionItem
                    {
                        CAction = item.CAction ?? "",
                        Responsible = item.Responsible ?? "",
                        CompletionDate = item.CompletionDate,
                        CorrectiveActionId = item.CorrectiveActionId
                    }).ToList() ?? new List<GetCorrectiveActionItem>()
                },

                IMVerification = request.IMVerification == null ? new GetIMVerification() : new GetIMVerification
                {
                    IsQA = request.IMVerification.IsQA,
                    QAReason = request.IMVerification.QAReason ?? "",
                    IsQB = request.IMVerification.IsQB,
                    QBReason = request.IMVerification.QBReason ?? "",
                    IsQC = request.IMVerification.IsQC,
                    QCReason = request.IMVerification.QCReason ?? "",
                    IsQD = request.IMVerification.IsQD,
                    QDReason = request.IMVerification.QDReason ?? "",
                    IsQE = request.IMVerification.IsQE,
                    QEReason = request.IMVerification.QEReason ?? "",
                    CourseOfAction = request.IMVerification.CourseOfAction ?? "",
                    CheckedBy = request.IMVerification.CheckedBy ?? ""
                },

                FollowUpStatus = request.FollowUpStatus == null ? new GetFollowUpStatus() : new GetFollowUpStatus
                {
                    F1Date = request.FollowUpStatus.F1Date,
                    F1Evidences = request.FollowUpStatus.F1Evidences ?? new List<string>(),
                    F1StatusOfActions = request.FollowUpStatus.F1StatusOfActions ?? "",
                    F1VerifiedBy = request.FollowUpStatus.F1VerifiedBy ?? "",
                    F2Date = request.FollowUpStatus.F2Date,
                    F2Evidences = request.FollowUpStatus.F2Evidences ?? new List<string>(),
                    F2StatusOfActions = request.FollowUpStatus.F2StatusOfActions ?? "",
                    F2VerifiedBy = request.FollowUpStatus.F2VerifiedBy ?? "",
                    F3Date = request.FollowUpStatus.F3Date,
                    F3Evidences = request.FollowUpStatus.F3Evidences ?? new List<string>(),
                    F3StatusOfActions = request.FollowUpStatus.F3StatusOfActions ?? "",
                    F3VerifiedBy = request.FollowUpStatus.F3VerifiedBy ?? ""
                },

                StatusOfEffectiveness = request.StatusOfEffectiveness == null ? new GetStatusOfEffectiveness() : new GetStatusOfEffectiveness
                {
                    IsS1 = request.StatusOfEffectiveness.IsS1,
                    IsS2 = request.StatusOfEffectiveness.IsS2,
                    IsS3 = request.StatusOfEffectiveness.IsS3,
                    VerifiedBy = request.StatusOfEffectiveness.VerifiedBy ?? "",
                    NotedBy = request.StatusOfEffectiveness.NotedBy ?? ""
                }
            };
        }

        public async Task<GetCARHeaderDTO?> GetSingleEntry(int Id)
        {
            GetCARHeaderDTO response = new GetCARHeaderDTO();

            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.StandardItems)
                        .ThenInclude(q => q.Standard)
                    .Include(q => q.NonConformityItems)
                        .ThenInclude(q => q.NonConformity)
                    .Include(q => q.IssuedTo)
                        .ThenInclude(q => q.IssuedToItems)
                    .Include(q => q.IssuedBy)
                        .ThenInclude(q => q.IssuedByItems)
                    .Include(q => q.ReturnComments)
                    .Include(q => q.CARType)
                    .Include(q => q.DetailsOfIssue)
                    .Include(q => q.ImmediateCorrection)
                    .Include(q => q.EliminationNonConformity)
                    .Include(q => q.CorrectiveAction)
                        .ThenInclude(q => q.CorrectiveActionItems)
                    .Include(q => q.IMVerification)
                    .Include(q => q.FollowUpStatus)
                    .Include(q => q.StatusOfEffectiveness)
                    .OrderByDescending(q => q.Id)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return null;

                return ConvertCARHeaderDTO(query);
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<PaginatedTableResponse<GetCARListDTO>> GetPaginatedEntry(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetCARListDTO> response = new PaginatedTableResponse<GetCARListDTO>();

            try
            {
                IQueryable<CARHeader> query = _context.CARHeaders
                    .Include(q => q.DetailsOfIssue)
                    .Include(q => q.NonConformityItems)
                    .ThenInclude(q => q.NonConformity)
                    .OrderByDescending(q => q.Id);

                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(q => q.Id.ToString() == request.SearchValue
                    || q.SysRefNo.Contains(request.SearchValue));
                }

                List<CARHeader> db_result = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.Count = await query.CountAsync();
                response.ResponseData = db_result.Select(q => new GetCARListDTO
                {
                    Id = q.Id,
                    CARNo = string.IsNullOrEmpty(q.CARNo) ? "-" : q.CARNo,
                    SysRefNo = string.IsNullOrEmpty(q.SysRefNo) ? "-" : q.SysRefNo,
                    Status = q.Status,
                    UnmentDept = "-",
                    IssuanceDate = q.IssuanceDate,

                    CARDetails = string.IsNullOrEmpty(q.DetailsOfIssue?.DetailsOfIssueDescription)
                    ? "-"
                    : q.DetailsOfIssue.DetailsOfIssueDescription,

                    DetailsOfConformity = q.NonConformityItems != null && q.NonConformityItems.Any()
                    ? string.Join(", ", q.NonConformityItems
                        .Where(n => n.NonConformity != null && !string.IsNullOrEmpty(n.NonConformity.Code))
                        .Select(n => n.NonConformity.Code))
                    : "-"
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<int> NotifyHead(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.NOTIFIED_HEAD;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> ReturnEntry(int Id, CreateReturnCommentDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .FirstOrDefaultAsync(q => q.Id == Id);

                string? userQuery = await _context.UserDetails
                    .Where(q => q.UserId.ToString() == GetUserId())
                    .Select(q => q.FirstName + " " + q.LastName)
                    .FirstOrDefaultAsync();

                if (query == null) return 0;

                if(query.Status == CARStatus.UPLOAD_EVIDENCE_OF_FIRST_FOLLOW_UP 
                    || query.Status == CARStatus.UPLOAD_EVIDENCE_OF_SECOND_FOLLOW_UP
                    || query.Status == CARStatus.UPLOAD_EVIDENCE_OF_THIRD_FOLLOW_UP)
                {
                    query.RevisionNo = (query.RevisionNo ?? 0) + 1;
                    query.RevisionDate = DateTime.Now;  
                }
                query.Status = CARStatus.OPEN;

                ReturnComment comment = new ReturnComment
                {
                    CARHeaderId = query.Id,
                    From = userQuery,
                    Reason = request.Reason,
                    ReturnedDate = DateTime.Now
                };
                _context.ReturnComments.Add(comment);   

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> ApproveEntry(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.EliminationNonConformity)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.SUBMITTED;
                query.EliminationNonConformity.ApprovalDate = DateTime.Now;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> EditEntry(int Id, CreateCARHeaderDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.StandardItems)
                        .ThenInclude(q => q.Standard)
                    .Include(q => q.NonConformityItems)
                        .ThenInclude(q => q.NonConformity)
                    .Include(q => q.IssuedTo)
                        .ThenInclude(q => q.IssuedToItems)
                    .Include(q => q.IssuedBy)
                        .ThenInclude(q => q.IssuedByItems)
                    .Include(q => q.ReturnComments)
                    .Include(q => q.CARType)
                    .Include(q => q.DetailsOfIssue)
                    .Include(q => q.ImmediateCorrection)
                    .Include(q => q.EliminationNonConformity)
                    .Include(q => q.CorrectiveAction)
                        .ThenInclude(q => q.CorrectiveActionItems)
                    .Include(q => q.IMVerification)
                    .Include(q => q.FollowUpStatus)
                    .Include(q => q.StatusOfEffectiveness)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                // Basic fields
                query.SysRefNo = request.SysRefNo;
                query.CARNo = request.CARNo;
                query.RevisionNo = request.RevisionNo;
                query.RevisionDate = request.RevisionDate;
                query.Recurring = request.Recurring;
                query.NonRecurring = request.NonRecurring;
                query.IssuanceDate = request.IssuedDate;
                query.Clauses = request.Clauses;
                query.CARTypeId = request.CARTypeId;
                query.TypeOfFinding = request.TypeOfFinding;
                query.TypeOfAccident = request.TypeOfAccident;
                query.Status = request.Status;

                // --- Issued By ---
                if (query.IssuedBy != null)
                {
                    query.IssuedBy.DepartmentId = request.CARIssuedBy.DepartmentId;
                    query.IssuedBy.IssuedByItems.Clear();
                    foreach (var item in request.CARIssuedBy.IssuedByItems)
                    {
                        query.IssuedBy.IssuedByItems.Add(new IssuedByItem { DSectionId = item.DSectionId });
                    }
                }

                // --- Issued To ---
                if (query.IssuedTo != null)
                {
                    query.IssuedTo.DepartmentId = request.CARIssuedTo.DepartmentId;
                    query.IssuedTo.IssuedToItems.Clear();
                    foreach (var item in request.CARIssuedTo.IssuedToItems)
                    {
                        query.IssuedTo.IssuedToItems.Add(new IssuedToItem { DSectionId = item.DSectionId });
                    }
                }

                // --- Standard Items ---
                query.StandardItems.Clear();
                foreach (var item in request.StandardItems)
                {
                    query.StandardItems.Add(new StandardItem
                    {
                        CARHeaderId = query.Id,
                        StandardId = item.StandardId
                    });
                }

                // --- Non-Conformity Items ---
                query.NonConformityItems.Clear();
                foreach (var item in request.NonConformityItems)
                {
                    query.NonConformityItems.Add(new NonConformityItem
                    {
                        CARHeaderId = query.Id,
                        NonConformityId = item.NonConformityId
                    });
                }

                // --- Details of Issue ---
                query.DetailsOfIssue ??= new DetailsOfIssue();
                query.DetailsOfIssue.DetailsOfIssueDescription = request.DetailsOfIssue.DetailsOfIssueDescription;
                query.DetailsOfIssue.DetailsOfIssueFiles = request.DetailsOfIssue.DetailsOfIssueFiles;
                query.DetailsOfIssue.EvidenceDescription = request.DetailsOfIssue.EvidenceDescription;
                query.DetailsOfIssue.EvidenceFiles = request.DetailsOfIssue.EvidenceFiles;
                query.DetailsOfIssue.RequirementsDescription = request.DetailsOfIssue.RequirementsDescription;
                query.DetailsOfIssue.RequirementsFiles = request.DetailsOfIssue.RequirementsFiles;

                // --- Immediate Correction ---
                query.ImmediateCorrection ??= new ImmediateCorrection();
                query.ImmediateCorrection.ActionsToCorrectDescription = request.ImmediateCorrection.ActionsToCorrectDescription;
                query.ImmediateCorrection.ActionsToCorrectFiles = request.ImmediateCorrection.ActionsToCorrectFiles;
                query.ImmediateCorrection.ActionsToDealDescription = request.ImmediateCorrection.ActionsToDealDescription;
                query.ImmediateCorrection.ActionsToDealFiles = request.ImmediateCorrection.ActionsToDealFiles;

                // --- Elimination of Non-Conformity ---
                query.EliminationNonConformity ??= new EliminationNonConformity();
                query.EliminationNonConformity.IsSimilarSituation = request.EliminationNonConformity.IsSimilarSituation;
                query.EliminationNonConformity.DepartmentId = request.EliminationNonConformity.DepartmentId;
                query.EliminationNonConformity.IsSimilarSituationDescription = request.EliminationNonConformity.IsSimilarSituationDescription;
                query.EliminationNonConformity.IsSimilarSituationFiles = request.EliminationNonConformity.IsSimilarSituationFiles;
                query.EliminationNonConformity.IsWhyWhy = request.EliminationNonConformity.IsWhyWhy;
                query.EliminationNonConformity.IsFishBone = request.EliminationNonConformity.IsFishBone;
                query.EliminationNonConformity.IsFaultTree = request.EliminationNonConformity.IsFaultTree;
                query.EliminationNonConformity.IsOthers = request.EliminationNonConformity.IsOthers;
                query.EliminationNonConformity.MethodFiles = request.EliminationNonConformity.MethodFiles;
                query.EliminationNonConformity.IsOthersDescription = request.EliminationNonConformity.IsOthersDescription;
                query.EliminationNonConformity.RootCaseDescription = request.EliminationNonConformity.RootCaseDescription;
                query.EliminationNonConformity.ApprovedBy = request.EliminationNonConformity.ApprovedBy;
                query.EliminationNonConformity.ApprovalDate = request.EliminationNonConformity.ApprovalDate;
                query.EliminationNonConformity.AnalyzedBy = request.EliminationNonConformity.AnalyzedBy;
                query.EliminationNonConformity.AnalyzedDate = request.EliminationNonConformity.AnalyzedDate;
                query.EliminationNonConformity.WorkerRepresentative = request.EliminationNonConformity.WorkerRepresentative;
                query.EliminationNonConformity.ReviewedBy = request.EliminationNonConformity.ReviewedBy;
                query.EliminationNonConformity.Designation = request.EliminationNonConformity.Designation;
                query.EliminationNonConformity.ReviewedDate = request.EliminationNonConformity.ReviewedDate;

                // --- Corrective Action ---
                query.CorrectiveAction ??= new CorrectiveAction();
                query.CorrectiveAction.CorrectiveActionItems.Clear();
                foreach (var item in request.CorrectiveAction.CorrectiveActionItems)
                {
                    query.CorrectiveAction.CorrectiveActionItems.Add(new CorrectiveActionItem
                    {
                        CAction = item.CAction,
                        Responsible = item.Responsible,
                        CompletionDate = item.CompletionDate
                    });
                }
                query.CorrectiveAction.PersonResponsible = request.CorrectiveAction.PersonResponsible;
                query.CorrectiveAction.DepartmentHead = request.CorrectiveAction.DepartmentHead;
                query.CorrectiveAction.ReviewedBy = request.CorrectiveAction.ReviewedBy;
                query.CorrectiveAction.ReviewerDesignation = request.CorrectiveAction.ReviewerDesignation;
                query.CorrectiveAction.ReviewedDate = request.CorrectiveAction.ReviewedDate;
                query.CorrectiveAction.InternalCommunicationFiles = request.CorrectiveAction.InternalCommunicationFiles;
                query.CorrectiveAction.IsManagementOfChange = request.CorrectiveAction.IsManagementOfChange;
                query.CorrectiveAction.ManagementOfChangeFiles = request.CorrectiveAction.ManagementOfChangeFiles;

                // --- IM Verification ---
                query.IMVerification ??= new IMVerification();
                query.IMVerification.IsQA = request.IMVerification.IsQA;
                query.IMVerification.QAReason = request.IMVerification.QAReason;
                query.IMVerification.IsQB = request.IMVerification.IsQB;
                query.IMVerification.QBReason = request.IMVerification.QBReason;
                query.IMVerification.IsQC = request.IMVerification.IsQC;
                query.IMVerification.QCReason = request.IMVerification.QCReason;
                query.IMVerification.IsQD = request.IMVerification.IsQD;
                query.IMVerification.QDReason = request.IMVerification.QDReason;
                query.IMVerification.IsQE = request.IMVerification.IsQE;
                query.IMVerification.QEReason = request.IMVerification.QEReason;
                query.IMVerification.CourseOfAction = request.IMVerification.CourseOfAction;
                query.IMVerification.CheckedBy = request.IMVerification.CheckedBy;

                // --- Follow Up Status ---
                query.FollowUpStatus ??= new FollowUpStatus();
                query.FollowUpStatus.F1Date = request.FollowUpStatus.F1Date;
                query.FollowUpStatus.F1Evidences = request.FollowUpStatus.F1Evidences;
                query.FollowUpStatus.F1StatusOfActions = request.FollowUpStatus.F1StatusOfActions;
                query.FollowUpStatus.F1VerifiedBy = request.FollowUpStatus.F1VerifiedBy;

                query.FollowUpStatus.F2Date = request.FollowUpStatus.F2Date;
                query.FollowUpStatus.F2Evidences = request.FollowUpStatus.F2Evidences;
                query.FollowUpStatus.F2StatusOfActions = request.FollowUpStatus.F2StatusOfActions;
                query.FollowUpStatus.F2VerifiedBy = request.FollowUpStatus.F2VerifiedBy;

                query.FollowUpStatus.F3Date = request.FollowUpStatus.F3Date;
                query.FollowUpStatus.F3Evidences = request.FollowUpStatus.F3Evidences;
                query.FollowUpStatus.F3StatusOfActions = request.FollowUpStatus.F3StatusOfActions;
                query.FollowUpStatus.F3VerifiedBy = request.FollowUpStatus.F3VerifiedBy;

                // --- Status of Effectiveness ---
                query.StatusOfEffectiveness ??= new StatusOfEffectiveness();
                query.StatusOfEffectiveness.IsS1 = request.StatusOfEffectiveness.IsS1;
                query.StatusOfEffectiveness.IsS2 = request.StatusOfEffectiveness.IsS2;
                query.StatusOfEffectiveness.IsS3 = request.StatusOfEffectiveness.IsS3;
                query.StatusOfEffectiveness.VerifiedBy = request.StatusOfEffectiveness.VerifiedBy;
                query.StatusOfEffectiveness.NotedBy = request.StatusOfEffectiveness.NotedBy;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> ReviewEntry(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.IMVerification)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.REVIEWED;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> ProceedEntry(int Id, CreateCARHeaderDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.IMVerification)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.PROCEED;

                // --- IM Verification ---
                query.IMVerification ??= new IMVerification();
                query.IMVerification.IsQA = request.IMVerification.IsQA;
                query.IMVerification.QAReason = request.IMVerification.QAReason;
                query.IMVerification.IsQB = request.IMVerification.IsQB;
                query.IMVerification.QBReason = request.IMVerification.QBReason;
                query.IMVerification.IsQC = request.IMVerification.IsQC;
                query.IMVerification.QCReason = request.IMVerification.QCReason;
                query.IMVerification.IsQD = request.IMVerification.IsQD;
                query.IMVerification.QDReason = request.IMVerification.QDReason;
                query.IMVerification.IsQE = request.IMVerification.IsQE;
                query.IMVerification.QEReason = request.IMVerification.QEReason;
                query.IMVerification.CourseOfAction = request.IMVerification.CourseOfAction;
                query.IMVerification.CheckedBy = request.IMVerification.CheckedBy;
                query.IMVerification.CheckedDate = DateTime.Now;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> EditFirstFolowUp(int Id, CreateCARHeaderDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.FollowUpStatus)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.UPLOAD_EVIDENCE_OF_FIRST_FOLLOW_UP ;

                // --- Follow Up Status ---
                query.FollowUpStatus ??= new FollowUpStatus();
                query.FollowUpStatus.F1Date = request.FollowUpStatus.F1Date;
                query.FollowUpStatus.F1Evidences = request.FollowUpStatus.F1Evidences;
                query.FollowUpStatus.F1StatusOfActions = request.FollowUpStatus.F1StatusOfActions;
                query.FollowUpStatus.F1VerifiedBy = request.FollowUpStatus.F1VerifiedBy;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> VerifyFirstFollowUp(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.DONE_FIRST_FOLLOW_UP;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> EditSecondFolowUp(int Id, CreateCARHeaderDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.FollowUpStatus)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.UPLOAD_EVIDENCE_OF_SECOND_FOLLOW_UP;

                // --- Follow Up Status ---
                query.FollowUpStatus ??= new FollowUpStatus();
                query.FollowUpStatus.F2Date = request.FollowUpStatus.F2Date;
                query.FollowUpStatus.F2Evidences = request.FollowUpStatus.F2Evidences;
                query.FollowUpStatus.F2StatusOfActions = request.FollowUpStatus.F2StatusOfActions;
                query.FollowUpStatus.F2VerifiedBy = request.FollowUpStatus.F2VerifiedBy;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> VerifySecondFollowUp(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.DONE_SECOND_FOLLOW_UP;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> EditThirdFolowUp(int Id, CreateCARHeaderDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.FollowUpStatus)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.UPLOAD_EVIDENCE_OF_THIRD_FOLLOW_UP;

                // --- Follow Up Status ---
                query.FollowUpStatus ??= new FollowUpStatus();
                query.FollowUpStatus.F3Date = request.FollowUpStatus.F3Date;
                query.FollowUpStatus.F3Evidences = request.FollowUpStatus.F3Evidences;
                query.FollowUpStatus.F3StatusOfActions = request.FollowUpStatus.F3StatusOfActions;
                query.FollowUpStatus.F3VerifiedBy = request.FollowUpStatus.F3VerifiedBy;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> VerifyThirdFollowUp(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.DONE_THIRD_FOLLOW_UP;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> CloseEntry(int Id, CreateCARHeaderDTO request)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.StandardItems)
                    .Include(q => q.NonConformityItems)
                    .Include(q => q.CARType)
                    .Include(q => q.DetailsOfIssue)
                    .Include(q => q.ImmediateCorrection)
                    .Include(q => q.EliminationNonConformity)
                    .Include(q => q.CorrectiveAction).ThenInclude(q => q.CorrectiveActionItems)
                    .Include(q => q.IMVerification)
                    .Include(q => q.FollowUpStatus)
                    .Include(q => q.StatusOfEffectiveness)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                // --- Status of Effectiveness ---
                query.StatusOfEffectiveness ??= new StatusOfEffectiveness();
                query.StatusOfEffectiveness.IsS1 = request.StatusOfEffectiveness.IsS1;
                query.StatusOfEffectiveness.IsS2 = request.StatusOfEffectiveness.IsS2;
                query.StatusOfEffectiveness.IsS3 = request.StatusOfEffectiveness.IsS3;
                query.StatusOfEffectiveness.VerifiedBy = request.StatusOfEffectiveness.VerifiedBy;
                query.StatusOfEffectiveness.NotedBy = request.StatusOfEffectiveness.NotedBy;

                if (request.StatusOfEffectiveness.IsS1 == true || request.StatusOfEffectiveness.IsS2 == true)
                {
                    query.Status = CARStatus.CLOSE_EFFECTIVE;
                }

                if (request.StatusOfEffectiveness.IsS3 == true)
                {
                    query.Status = CARStatus.CLOSE_INEFFECTIVE;

                    var newHeader = CloneCARHeader(query, true, false);
                    _context.CARHeaders.Add(newHeader);
                }
                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> NoteEntry(int Id)
        {
            try
            {
                CARHeader? query = await _context.CARHeaders
                    .Include(q => q.StandardItems)
                        .ThenInclude(q => q.Standard)
                    .Include(q => q.NonConformityItems)
                        .ThenInclude(q => q.NonConformity)
                    .Include(q => q.IssuedTo)
                        .ThenInclude(q => q.IssuedToItems)
                    .Include(q => q.IssuedBy)
                        .ThenInclude(q => q.IssuedByItems)
                    .Include(q => q.ReturnComments)
                    .Include(q => q.CARType)
                    .Include(q => q.DetailsOfIssue)
                    .Include(q => q.ImmediateCorrection)
                    .Include(q => q.EliminationNonConformity)
                    .Include(q => q.CorrectiveAction)
                        .ThenInclude(q => q.CorrectiveActionItems)
                    .Include(q => q.IMVerification)
                    .Include(q => q.FollowUpStatus)
                    .Include(q => q.StatusOfEffectiveness)
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Status = CARStatus.NOTED;

                var newHeader = CloneCARHeader(query, false, true);
                _context.CARHeaders.Add(newHeader);

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private CARHeader CloneCARHeader(CARHeader source, bool IsIneffective, bool IsArchive)
        {
            return new CARHeader
            {
                SysRefNo = source.SysRefNo,
                CARNo = source.CARNo,
                RevisionNo = source.RevisionNo,
                RevisionDate = source.RevisionDate,
                Recurring = source.Recurring,
                NonRecurring = source.NonRecurring,
                IssuanceDate = source.IssuanceDate,
                Clauses = source.Clauses,
                CARType = source.CARType,
                CARTypeId = source.CARTypeId,
                TypeOfFinding = source.TypeOfFinding,
                TypeOfAccident = source.TypeOfAccident,
                Status = source.Status,
                CreatedBy = source.CreatedBy,
                DateCreated = DateTime.Now,
                IsIneffective = IsIneffective,
                IsArchive = IsArchive,

                // Clone Standard Items
                StandardItems = source.StandardItems?
                    .Select(s => new StandardItem
                    {
                        StandardId = s.StandardId
                    }).ToList() ?? new(),

                // Clone Non-Conformity Items
                NonConformityItems = source.NonConformityItems?
                    .Select(n => new NonConformityItem
                    {
                        NonConformityId = n.NonConformityId
                    }).ToList() ?? new(),

                IssuedBy = new IssuedBy
                {
                    DepartmentId = source.IssuedBy.DepartmentId,
                    IssuedByItems = source.IssuedBy.IssuedByItems.Select(item => new IssuedByItem
                    {
                        DSectionId = item.DSectionId
                    }).ToList()
                },
                IssuedTo = new IssuedTo
                {
                    DepartmentId = source.IssuedTo.DepartmentId,
                    IssuedToItems = source.IssuedTo.IssuedToItems.Select(item => new IssuedToItem
                    {
                        DSectionId = item.DSectionId
                    }).ToList()
                },

                // Clone DetailsOfIssue
                DetailsOfIssue = source.DetailsOfIssue != null ? new DetailsOfIssue
                {
                    DetailsOfIssueDescription = source.DetailsOfIssue.DetailsOfIssueDescription,
                    DetailsOfIssueFiles = source.DetailsOfIssue.DetailsOfIssueFiles,
                    EvidenceDescription = source.DetailsOfIssue.EvidenceDescription,
                    EvidenceFiles = source.DetailsOfIssue.EvidenceFiles,
                    RequirementsDescription = source.DetailsOfIssue.RequirementsDescription,
                    RequirementsFiles = source.DetailsOfIssue.RequirementsFiles
                } : new(),

                // Clone Immediate Correction
                ImmediateCorrection = source.ImmediateCorrection != null ? new ImmediateCorrection
                {
                    ActionsToCorrectDescription = source.ImmediateCorrection.ActionsToCorrectDescription,
                    ActionsToCorrectFiles = source.ImmediateCorrection.ActionsToCorrectFiles,
                    ActionsToDealDescription = source.ImmediateCorrection.ActionsToDealDescription,
                    ActionsToDealFiles = source.ImmediateCorrection.ActionsToDealFiles
                } : new(),

                // Clone Elimination of Non-Conformity
                EliminationNonConformity = source.EliminationNonConformity != null ? new EliminationNonConformity
                {
                    IsSimilarSituation = source.EliminationNonConformity.IsSimilarSituation,
                    DepartmentId = source.EliminationNonConformity.DepartmentId,
                    IsSimilarSituationDescription = source.EliminationNonConformity.IsSimilarSituationDescription,
                    IsSimilarSituationFiles = source.EliminationNonConformity.IsSimilarSituationFiles,
                    IsWhyWhy = source.EliminationNonConformity.IsWhyWhy,
                    IsFishBone = source.EliminationNonConformity.IsFishBone,
                    IsFaultTree = source.EliminationNonConformity.IsFaultTree,
                    IsOthers = source.EliminationNonConformity.IsOthers,
                    MethodFiles = source.EliminationNonConformity.MethodFiles,
                    IsOthersDescription = source.EliminationNonConformity.IsOthersDescription,
                    RootCaseDescription = source.EliminationNonConformity.RootCaseDescription,
                    ApprovedBy = source.EliminationNonConformity.ApprovedBy,
                    ApprovalDate = source.EliminationNonConformity.ApprovalDate,
                    AnalyzedBy = source.EliminationNonConformity.AnalyzedBy,
                    AnalyzedDate = source.EliminationNonConformity.AnalyzedDate,
                    WorkerRepresentative = source.EliminationNonConformity.WorkerRepresentative,
                    ReviewedBy = source.EliminationNonConformity.ReviewedBy,
                    Designation = source.EliminationNonConformity.Designation,
                    ReviewedDate = source.EliminationNonConformity.ReviewedDate
                } : new(),

                // Clone Corrective Action
                CorrectiveAction = source.CorrectiveAction != null ? new CorrectiveAction
                {
                    CorrectiveActionItems = source.CorrectiveAction.CorrectiveActionItems?
                        .Select(a => new CorrectiveActionItem
                        {
                            CAction = a.CAction,
                            Responsible = a.Responsible,
                            CompletionDate = a.CompletionDate
                        }).ToList() ?? new(),

                    PersonResponsible = source.CorrectiveAction.PersonResponsible,
                    DepartmentHead = source.CorrectiveAction.DepartmentHead,
                    ReviewedBy = source.CorrectiveAction.ReviewedBy,
                    ReviewerDesignation = source.CorrectiveAction.ReviewerDesignation,
                    ReviewedDate = source.CorrectiveAction.ReviewedDate,
                    InternalCommunicationFiles = source.CorrectiveAction.InternalCommunicationFiles,
                    IsManagementOfChange = source.CorrectiveAction.IsManagementOfChange,
                    ManagementOfChangeFiles = source.CorrectiveAction.ManagementOfChangeFiles
                } : new(),

                // Clone IMVerification
                IMVerification = source.IMVerification != null ? new IMVerification
                {
                    IsQA = source.IMVerification.IsQA,
                    QAReason = source.IMVerification.QAReason,
                    IsQB = source.IMVerification.IsQB,
                    QBReason = source.IMVerification.QBReason,
                    IsQC = source.IMVerification.IsQC,
                    QCReason = source.IMVerification.QCReason,
                    IsQD = source.IMVerification.IsQD,
                    QDReason = source.IMVerification.QDReason,
                    IsQE = source.IMVerification.IsQE,
                    QEReason = source.IMVerification.QEReason,
                    CourseOfAction = source.IMVerification.CourseOfAction,
                    CheckedBy = source.IMVerification.CheckedBy,
                } : new(),

                // Clone FollowUpStatus
                FollowUpStatus = source.FollowUpStatus != null ? new FollowUpStatus
                {
                    F1Date = source.FollowUpStatus.F1Date,
                    F1Evidences = source.FollowUpStatus.F1Evidences,
                    F1StatusOfActions = source.FollowUpStatus.F1StatusOfActions,
                    F1VerifiedBy = source.FollowUpStatus.F1VerifiedBy,
                    F2Date = source.FollowUpStatus.F2Date,
                    F2Evidences = source.FollowUpStatus.F2Evidences,
                    F2StatusOfActions = source.FollowUpStatus.F2StatusOfActions,
                    F2VerifiedBy = source.FollowUpStatus.F2VerifiedBy,
                    F3Date = source.FollowUpStatus.F3Date,
                    F3Evidences = source.FollowUpStatus.F3Evidences,
                    F3StatusOfActions = source.FollowUpStatus.F3StatusOfActions,
                    F3VerifiedBy = source.FollowUpStatus.F3VerifiedBy
                } : new(),

                // Clone StatusOfEffectiveness
                StatusOfEffectiveness = source.StatusOfEffectiveness != null ? new StatusOfEffectiveness
                {
                    IsS1 = source.StatusOfEffectiveness.IsS1,
                    IsS2 = source.StatusOfEffectiveness.IsS2,
                    IsS3 = source.StatusOfEffectiveness.IsS3,
                    VerifiedBy = source.StatusOfEffectiveness.VerifiedBy,
                    NotedBy = source.StatusOfEffectiveness.NotedBy
                } : new(),

                // Leave comments empty
                ReturnComments = new List<ReturnComment>()
            };
        }



    }
}
