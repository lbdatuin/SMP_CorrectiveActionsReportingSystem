using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Services.CAREntryService
{
    public interface ICAREntryService
    {
        Task<int> CreateEntry(CreateCARHeaderDTO request);
        Task<GetCARHeaderDTO?> GetSingleEntry(int Id);
        Task<PaginatedTableResponse<GetCARListDTO>> GetPaginatedEntry(GetPaginatedDTO request);
        Task<int> NotifyHead(int Id);
        Task<int> ReturnEntry(int Id, CreateReturnCommentDTO request);
        Task<int> ApproveEntry(int Id);
        Task<int> EditEntry(int Id, CreateCARHeaderDTO request);
        Task<int> ReviewEntry(int Id);
        Task<int> ProceedEntry(int Id, CreateCARHeaderDTO request);
        Task<int> EditFirstFolowUp(int Id, CreateCARHeaderDTO request);
        Task<int> VerifyFirstFollowUp(int Id);
        Task<int> EditSecondFolowUp(int Id, CreateCARHeaderDTO request);
        Task<int> VerifySecondFollowUp(int Id);
        Task<int> EditThirdFolowUp(int Id, CreateCARHeaderDTO request);
        Task<int> VerifyThirdFollowUp(int Id);
        Task<int> CloseEntry(int Id, CreateCARHeaderDTO request);
        Task<int> NoteEntry(int Id);
    }
}
