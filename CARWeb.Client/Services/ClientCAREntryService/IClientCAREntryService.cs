using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientCAREntryService
{
    public interface IClientCAREntryService
    {
        Task<int> CreateEntry(CreateCARHeaderDTO payload);
        Task<GetCARHeaderDTO?> GetSingleEntry(int Id);
        Task<PaginatedTableResponse<GetCARListDTO>> GetPaginatedEntry(GetPaginatedDTO payload);
    }
}
