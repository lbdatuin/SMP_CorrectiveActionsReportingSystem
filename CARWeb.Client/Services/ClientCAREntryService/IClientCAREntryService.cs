using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientCAREntryService
{
    public interface IClientCAREntryService
    {
        Task<int> CreateEntry(CreateCARHeaderDTO payload);
        Task<GetCARHeaderDTO?> GetSingleEntry(int Id);
        Task<PaginatedTableResponse<GetCARListDTO>> GetPaginatedEntry(GetPaginatedDTO payload);
        Task<int> NotifyHead(int Id);
        Task<int> ReturnEntry(int Id, CreateReturnCommentDTO payload);
        Task<int> ApproveEntry(int Id);
        Task<int> EditEntry(int Id, CreateCARHeaderDTO payload);
        Task<int> ReviewEntry(int Id);
        Task<int> ProceedEntry(int Id, CreateCARHeaderDTO payload);
    }
}
